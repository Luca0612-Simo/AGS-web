using AGS_Models;
using AGS_Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGS_services.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetUsers();
        public Task<UserResultDTO> CreateUser(User user);
        public Task<UserResultDTO> Login(UserDTO user);
        public Task<string> GetUserById(int id);
        public Task<bool> ChangePass(string pass,int id);
    }
}
