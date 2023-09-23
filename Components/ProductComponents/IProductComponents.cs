using DataAccess.Data;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components.ProductComponents
{
    public interface IProductComponents
    {
        Task<List<Medicine>> GetAllProducts();
        Task<RestFromBusiness> DeleteProduct(Guid productID, Guid LoggedInUser);
        Task<RestFromBusiness> CreateProduct(Medicine Medicine, Guid LoggedInUser);
        Task<RestFromBusiness> UpdateProduct(Medicine Medicine, Guid LoggedInUser);
        Task<Medicine> GetProduct(Guid productID);
        Task<RestFromBusiness> AddStock(Stock Stock, Guid LoggedInUser);
        Task<RestFromBusiness> AddPriceList(PriceList PriceList, Guid LoggedInUser);

    }
}
