using AGS_Models;
using AGS_Models.DTO;
using AGS_services.Handler;
using AGS_services.Repositories;
using AGS_services.Validators;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_services
{
    public class UserService : IUserRepository
    {
        public async Task<UserResultDTO> CreateUser(User user)
        {

            UserResultDTO user_result = new UserResultDTO();

            UserValidator validator = new UserValidator();
            var validationResult = validator.Validate(user);

            if (!validationResult.IsValid)
            {
                user_result.Result = false;
                user_result.Message = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            }
            else
            {
                string query = $"SELECT COUNT(*) FROM usuarios WHERE mail = '{user.mail}';";
                string result = MySqlHandler.GetScalar(query);

                if (result == "0")
                {
                    var hashed = BCrypt.Net.BCrypt.HashPassword(user.contrasena);
                    query = $"INSERT INTO usuarios (id,nombre,apellido,mail,telefono,contrasena)" +
                        $"VALUES (null,'{user.nombre}','{user.apellido}','{user.mail}','{user.telefono}','{hashed}');";
                    bool b_result = MySqlHandler.Exec(query);

                    if (b_result)
                    {
                        user_result.Result = true;
                        user_result.Message = $"Usuario creado correctamente, se notificara al correo '{user.mail}'";
                    }
                    else
                    {
                        user_result.Result = false;
                        user_result.Message = "Hubo un problema al crear el usuario, intente nuevamente";
                    }
                }
                else
                {
                    user_result.Result = false;
                    user_result.Message = "Ya existe un usuario con ese correo, por favor ingrese otro";
                }
            }

            return user_result;
        }

        public async Task<List<User>> GetUsers()
        {
            string query = "SELECT * FROM usuarios;";
            string json = MySqlHandler.GetJson(query);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
            return users;
        }

        public async Task<UserResultDTO> Login(UserDTO user)
        {
            string query = $"SELECT COUNT(*) FROM usuarios WHERE mail = '{user.mail}';";
            string result = MySqlHandler.GetScalar(query);

            UserResultDTO login_result = new UserResultDTO();

            if (result == "0")
            {
                login_result.Result = false;
                login_result.Message = "No se encontró un usuario con ese correo.";
            }
            else
            {
                query = $"SELECT contrasena FROM usuarios WHERE mail = '{user.mail}';";
                string hash = MySqlHandler.GetScalar(query);
                bool compare = BCrypt.Net.BCrypt.Verify(user.pass, hash);
                if (compare)
                {
                    login_result.Result = true;
                    login_result.Message = "Bienvenido!";

                    query = $"SELECT requiere_cambio_contrasena FROM usuarios WHERE mail = '{user.mail}';";
                    result = MySqlHandler.GetScalar(query);
                    bool requiere_cambio = result == "1" || result.ToLower() == "true";

                    if (requiere_cambio)
                    {
                        //Ver como manejo esto desde el front...
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
    }
}
