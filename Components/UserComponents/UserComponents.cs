using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.UserComponents
{
    public class UserComponents : IUserComponents
    {
        private readonly BoyutCaseContext _context;

        public UserComponents(BoyutCaseContext context)
        {
            _context = context;
        }

        public async Task<Users> GetUserByUserName(string UserName)
        {
            return await _context.Users.Where(x => x.Username == UserName)
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync(x => x.Username == UserName);
        }
    }
}
