using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;

namespace Components.UserComponents
{
    public interface IUserComponents
    {
        Task<Users> GetUserByUserName(string UserName);
    }
}
