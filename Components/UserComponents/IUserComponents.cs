using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using Model;

namespace Components.UserComponents
{
    public interface IUserComponents
    {
        Task<Users> GetUserByUserName(string UserName);
        Task<Users> GetUserByID(Guid userID);
        Task<List<Users>> GetAllUsers();
        Task<RestFromBusiness> DeleteUser(Guid userID,Guid LoggedInUser);
        Task<RestFromBusiness> CreateUser(Users User, Guid LoggedInUser);
        Task<RestFromBusiness> UpdateUser(Users User, Guid LoggedInUser);

    }
}
