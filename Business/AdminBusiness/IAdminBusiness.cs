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

        Task<List<ProductDTO>> GetProducts();
        Task<RestFromBusiness> DeleteProduct(Guid productID, Guid LoggedInUser);
        Task<RestFromBusiness> CreateProduct(Medicine Medicine, Guid LoggedInUser);
        Task<RestFromBusiness> UpdateProduct(Medicine Medicine, Guid LoggedInUser);
        Task<Medicine> GetProduct(Guid productID);
        Task<RestFromBusiness> AddStock(Stock stock, Guid LoggedInUser);
        Task<RestFromBusiness> AddPriceList(PriceList addPriceListRequest, Guid LoggedInUser);

    }
}
