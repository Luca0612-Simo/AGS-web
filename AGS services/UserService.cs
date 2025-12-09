using AGS_Models;
using AGS_Models.DTO;
using AGS_services.Repositories;
using AGS_services.Validators;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens; 
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text; 
using Microsoft.EntityFrameworkCore;
using AGS_models.DTO;
using Microsoft.AspNetCore.Http; 
using System.Security.Claims;

namespace AGS_services
{
    public class UserService : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IConfiguration configuration, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int? GetCurrentUserId()
        {
            var idClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim != null && int.TryParse(idClaim.Value, out int userId))
            {
                return userId;
            }
            return null; 
        }

        public async Task<UserResultDTO> Login(UserDTO userDto)
        {
            UserResultDTO login_result = new UserResultDTO();
            User userFromDb = await _context.Usuarios.FirstOrDefaultAsync(u => u.mail == userDto.mail && !u.estaEliminado);

            if (userFromDb == null)
            {
                login_result.Result = false;
                login_result.Message = "No se encontró un usuario activo con ese correo.";
            }
            else
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(userDto.pass, userFromDb.contrasena);

                if (isPasswordCorrect)
                {
                    string token = GenerateJwtToken(userFromDb); 

                    login_result.Result = true;
                    login_result.Message = "Bienvenido!";
                    login_result.token = token; 

                    bool requiere_cambio = userFromDb.requiere_cambio_contrasena == "1" ||
                                           userFromDb.requiere_cambio_contrasena?.ToLower() == "true";

                    if (requiere_cambio)
                    {
                        login_result.contrasena = true;
                    }
                }
                else
                {
                    login_result.Result = false;
                    login_result.Message = "Contraseña incorrecta.";
                }
            }
            return login_result;
        }

        public async Task<UserResultDTO> CreateUser(UserCreateDTO userDTO)
        {
            UserResultDTO user_result = new UserResultDTO();
            UserValidator validator = new UserValidator();
            var validationResult = validator.Validate(userDTO);

            if (!validationResult.IsValid)
            {
                user_result.Result = false;
                user_result.Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return user_result;
            }

            if (await _context.Usuarios.AnyAsync(u => u.mail == userDTO.mail))
            {
                user_result.Result = false;
                user_result.Message = "Ya existe un usuario con ese correo, por favor ingrese otro";
                return user_result;
            }

            var newUser = new User
            {
                nombre = userDTO.nombre,
                apellido = userDTO.apellido,
                mail = userDTO.mail,
                telefono = userDTO.telefono,
                requiere_cambio_contrasena = userDTO.requiere_cambio_contrasena,

                contrasena = BCrypt.Net.BCrypt.HashPassword(userDTO.contrasena),

                fechaAlta = DateOnly.FromDateTime(DateTime.UtcNow), 
                creadoPor = GetCurrentUserId(),
                estaEliminado = false,
            };

            await _context.Usuarios.AddAsync(newUser);
            await _context.SaveChangesAsync();

            user_result.Result = true;
            user_result.Message = $"Usuario creado correctamente, se notificara al correo '{newUser.mail}'";

            return user_result;           
        }

        public async Task<IEnumerable<User>> GetUsersByStatus(string statusFilter)
        {
            var query = _context.Usuarios.AsQueryable();

            if (statusFilter.ToLower() == "activo")
            {
                query = query.Where(u => !u.estaEliminado);
            }
            else if (statusFilter.ToLower() == "inactivo")
            {
                query = query.Where(u => u.estaEliminado);
            }

            return await query.ToListAsync();
        }

        private string GenerateJwtToken(User user)
        {
            var issuer = _configuration["Jwt:Issuer"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, user.mail),
                new Claim(JwtRegisteredClaimNames.GivenName, user.nombre),
                new Claim(JwtRegisteredClaimNames.NameId, user.id.ToString()),
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = issuer,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.id == id);
            return user;
        }

        public async Task<UserResultDTO> UpdateUser(int id, UserProfileDTO userDTO)
        {
            var user_result = new UserResultDTO();
            var userFromDb = await _context.Usuarios.FindAsync(id);

            if (userFromDb == null || userFromDb.estaEliminado)
            {
                user_result.Result = false;
                user_result.Message = "El usuario no se encontró";
                return user_result;
            }

            var validator = new UserProfileDTOValidator();
            var validationResult = validator.Validate(userDTO);

            if (!validationResult.IsValid)
            {
                user_result.Result = false;
                user_result.Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return user_result;
            }

            if (!string.IsNullOrEmpty(userDTO.nombre))
            {
                userFromDb.nombre = userDTO.nombre;
            }
            if (!string.IsNullOrEmpty(userDTO.apellido))
            {
                userFromDb.apellido = userDTO.apellido;
            }
            if (!string.IsNullOrEmpty(userDTO.mail))
            {
                userFromDb.mail = userDTO.mail;
            }
            if (!string.IsNullOrEmpty(userDTO.telefono))
            {
                userFromDb.telefono = userDTO.telefono;
            }

            await _context.SaveChangesAsync();

            user_result.Result = true;
            user_result.Message = "Usuario actualizado correctamente";
            return user_result;
        }

        public async Task<UserResultDTO> DeleteUser(int id)
        {
            var user_result = new UserResultDTO();
            var userFromDb = await _context.Usuarios.FindAsync(id);

            if (userFromDb == null || userFromDb.estaEliminado)
            {
                user_result.Result = false;
                user_result.Message = "No se encontró el usuario a eliminar";
                return user_result;
            }

            userFromDb.estaEliminado = true; 
            userFromDb.fechaBaja = DateOnly.FromDateTime(DateTime.UtcNow);
            userFromDb.eliminadoPor = GetCurrentUserId();

            await _context.SaveChangesAsync(); 

            user_result.Result = true;
            user_result.Message = "Usuario eliminado correctamente";
            return user_result;
        }

        public async Task<UserResultDTO> ChangePass(int id, ChangePassDTO passDto)
        {
            var user_result = new UserResultDTO();

            var validator = new ChangePassDTOValidator();
            var validationResult = validator.Validate(passDto);

            if (!validationResult.IsValid)
            {
                user_result.Result = false;
                user_result.Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                return user_result;
            }

            var userFromDb = await _context.Usuarios.FindAsync(id);
            if (userFromDb == null)
            {
                user_result.Result = false;
                user_result.Message = "No se encontro el usuario";
                return user_result;
            }

            userFromDb.contrasena = BCrypt.Net.BCrypt.HashPassword(passDto.NewPassword);
            userFromDb.requiere_cambio_contrasena = "false";

            await _context.SaveChangesAsync();

            user_result.Result = true;
            user_result.Message = "Contraseña actualizada correctamente";
            return user_result;
        }
    }
}