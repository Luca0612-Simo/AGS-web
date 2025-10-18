using AGS_Models;
using AGS_Models.DTO;
using AGS_services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Proyectos_AGS.Controllers
{
    [Route("AGS/users")]
    [ApiController]
    public class UserController :ControllerBase
    {
        private readonly IUserRepository _UserService;

        public UserController(IUserRepository userService)
        {
            _UserService = userService;
        }

        [HttpGet("GetUsers")]
        public async Task<List<User>> GetUsers()
        {
            return await Task.Run(() => _UserService.GetUsers());
        }

        [HttpGet("GetUserById")]
        public async Task<string> GetUserById(int id)
        {
            return await Task.Run(() => _UserService.GetUserById(id));
        }

        [HttpPost("CreateUser")]
        public async Task<UserResultDTO> CreateUser(User user)
        {
            return await Task.Run(()=> _UserService.CreateUser(user));
        }

        [HttpPost("Login")]
        public async Task<UserResultDTO> Login(UserDTO user)
        {
            return await Task.Run(() => _UserService.Login(user));
        }

        [HttpPut("ChangePass")]
        public async Task<bool> ChangePass(string pass, int id)
        {
            return await Task.Run(() => _UserService.ChangePass(pass, id));
        }
    }
}
