using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Model;
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
            return await _context.Users.Where(x => x.Username == UserName && !x.IsDeleted)
                .Include(x => x.UserRole)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _context.Users.Where(x => !x.IsDeleted)
                .Include(x => x.UserRole)
                .ToListAsync();
        }

        public async Task<RestFromBusiness> DeleteUser(Guid userID, Guid LoggedInUser)
        {
            try
            {
                Users user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userID);
                if (user != null)
                {
                    user.IsDeleted = true;
                    user.UpdatedDate = DateTime.Now;
                    user.UpdatedBy = LoggedInUser;
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return new RestFromBusiness()
                    {
                        Message = "Silme işlemi başarılı",
                        StatusCode = 200,
                    };
                }
                else
                {
                    throw new ApplicationException("Kullanıcı bulunamadı");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }

        }

        public async Task<Users> GetUserByID(Guid UserID)
        {
            return await _context.Users.Where(x => x.Id == UserID && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<RestFromBusiness> CreateUser(Users User, Guid LoggedInUser)
        {
            try
            {
                User.Id = Guid.NewGuid();
                User.CreatedDate = DateTime.Now;
                User.UpdatedDate = DateTime.Now;
                User.UpdatedBy = LoggedInUser;
                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();
                return new RestFromBusiness()
                {
                    Message = "Kayıt işlemi başarılı",
                    StatusCode = 200,
                };

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public async Task<RestFromBusiness> UpdateUser(Users User, Guid LoggedInUser)
        {
            try
            {
                Users existUser = await _context.Users.FirstOrDefaultAsync(c => c.Id == User.Id);
                if (existUser != null)
                {

                    existUser.Name = User.Name;
                    existUser.Username = User.Username;
                    existUser.UserRoleId = User.UserRoleId;
                    existUser.Surname = User.Surname;
                    existUser.Password = User.Password;
                    existUser.UpdatedDate = DateTime.Now;
                    existUser.UpdatedBy = LoggedInUser;
                    _context.Entry(existUser).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return new RestFromBusiness()
                    {
                        Message = "Güncelleme işlemi başarılı",
                        StatusCode = 200,
                    };
                }
                else
                {
                    throw new ApplicationException("Kullanıcı bulunamadı");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
