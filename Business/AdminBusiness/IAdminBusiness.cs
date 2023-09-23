using DataAccess.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.AdminBusiness
{
    public interface IAdminBusiness
    {
        Task<List<UserDTO>> GetUsers();
        Task<RestFromBusiness> DeleteUser(Guid userID, Guid LoggedInUser);
        Task<RestFromBusiness> CreateUser(Users User, Guid LoggedInUser);
        Task<RestFromBusiness> UpdateUser(Users User, Guid LoggedInUser);
        Task<Users> GetUser(Guid userID);
    }
}
