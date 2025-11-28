using AGS_models.DTO;
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
        public Task<User?> GetUserById(int id);
        public Task<UserResultDTO> UpdateUser(int id, UserProfileDTO userDTO);
        public Task<UserResultDTO> DeleteUser(int id);
        public Task<UserResultDTO> ChangePass(int id, ChangePassDTO passDto);
    }
}

