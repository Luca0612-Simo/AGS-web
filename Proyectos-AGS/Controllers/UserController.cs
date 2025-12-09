using AGS_models.DTO;
using AGS_Models;
using AGS_Models.DTO;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Proyectos_AGS.Controllers
{
    [Route("AGS/users")]
    [ApiController]

    public class UserController : ControllerBase

    {
        private readonly IUserRepository _UserService;

        public UserController(IUserRepository userService)
        {
            _UserService = userService;
        }

        /// <summary>
        /// Devuelve usuarios filtrados por estado.
        /// </summary>
        /// <param name="status">Opciones: 'activo', 'inactivo'. Por defecto es 'activo'.</param>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsersByStatus([FromQuery] string status = "activo")
        {
            var users = await _UserService.GetUsersByStatus(status);

            return Ok(users);
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <returns>El resultado de la operación.</returns>
        [HttpPost("CreateUser")]
        public async Task<UserResultDTO> CreateUser(UserCreateDTO user)
        {
            return await Task.Run(() => _UserService.CreateUser(user));

        }

        /// <summary>
        /// Inicia sesión y genera un Token JWT.
        /// </summary>
        /// <returns>Devuleve un Token si las credenciales son válidas.</returns>
        [HttpPost("Login")]
        public async Task<UserResultDTO> Login(UserDTO user)
        {
            return await Task.Run(() => _UserService.Login(user));
        }

        /// <summary>
        /// Devuelve un usuario  por su ID.
        /// </summary>
        /// <returns>   User si existe, o 404 Not Found.</returns>
        [HttpGet("{id}")]
        [Authorize] 
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _UserService.GetUserById(id);
            if (user == null)
            {
                return NotFound(new { message = "Usuario no encontrado" });
            }
            return Ok(user);
        }

        /// <summary>
        /// Actualiza parcialmente los datos del perfil de un usuario.
        /// </summary>
        /// <remarks>
        /// No podes cambiar la contraseña desde aca. Usar ChangePass para eso.
        /// </remarks>
        [HttpPatch("{id}")]
        [Authorize] 
        public async Task<IActionResult> UpdateUser(int id, [FromBody]UserProfileDTO userDTO)
        {
            var result = await _UserService.UpdateUser(id, userDTO);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Da de baja lógica a un usuario por su id
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize] 
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _UserService.DeleteUser(id);
            if (!result.Result)
            {
                return NotFound(result); 
            }
            return Ok(result);
        }

        /// <summary>
        /// Cambia la contraseña de un usuario.
        /// </summary>
        [HttpPost("ChangePass/{id}")]
        [Authorize] 
        public async Task<IActionResult> ChangePass(int id, [FromBody] ChangePassDTO passDto)
        {
            var result = await _UserService.ChangePass(id, passDto);
            if (!result.Result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
