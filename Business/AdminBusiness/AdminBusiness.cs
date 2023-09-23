using Business.Helpers;
using Components.UserComponents;
using DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AdminBusiness
{
    public class AdminBusiness : IAdminBusiness
    {
        private readonly IUserComponents _userComponents;
        private readonly IConfiguration _config;
        public AdminBusiness(IUserComponents userComponents, IConfiguration config)
        {
            _userComponents = userComponents;
            _config = config;
        }


        public async Task<List<UserDTO>> GetUsers()
        {
            List<Users> users = await _userComponents.GetAllUsers();

            return users.Select(c => new UserDTO
            {
                ID = c.Id,
                Name = c.Name,
                Role = c.UserRole.RoleName,
                Surname = c.Surname,
                Username = c.Username
            }).ToList();

        }
        public async Task<RestFromBusiness> DeleteUser(Guid userID, Guid LoggedInUser)
        {
            return await _userComponents.DeleteUser(userID, LoggedInUser);
        }

        public async Task<Users> GetUser(Guid userID)
        {
            Users user = await _userComponents.GetUserByID(userID);
            user.Password = EncryptionHelper.Decrypt(user.Password, user.Id.ToString());
            return user;
        }

        public async Task<RestFromBusiness> CreateUser(Users User, Guid LoggedInUser)
        {
            User.Password = EncryptionHelper.Encrypt(User.Password, User.Id.ToString());
            return await _userComponents.CreateUser(User, LoggedInUser);
        }

        public async Task<RestFromBusiness> UpdateUser(Users User, Guid LoggedInUser)
        {
            User.Password = EncryptionHelper.Encrypt(User.Password, User.Id.ToString());
            return await _userComponents.UpdateUser(User, LoggedInUser);
        }
    }
}
