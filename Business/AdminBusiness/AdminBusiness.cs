using Business.Helpers;
using Components.ProductComponents;
using Components.UserComponents;
using DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Enums;

namespace Business.AdminBusiness
{
    public class AdminBusiness : IAdminBusiness
    {
        private readonly IUserComponents _userComponents;
        private readonly IProductComponents _productComponents;
        private readonly IConfiguration _config;
        public AdminBusiness(IUserComponents userComponents, IProductComponents productComponents, IConfiguration config)
        {
            _userComponents = userComponents;
            _productComponents = productComponents;
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
        public async Task<List<ProductDTO>> GetProducts()
        {
            List<Medicine> products = await _productComponents.GetAllProducts();
            return products.Select(product => new ProductDTO
            {
                ListPrice = product.PriceList.Count != 0 ? product.PriceList.FirstOrDefault().Price : 0,
                MedicineID = product.Id,
                MedicineName = product.MedicineName,
                UniqueCode = product.UniqueCode,
                StockAmount = product.Stock.Count != 0 ? product.Stock.Where(c => c.Direction == (byte)StockDirections.Purchase).Sum(c => c.Piece) - product.Stock.Where(c => c.Direction == (byte)StockDirections.Sale).Sum(c => c.Piece) : 0,
            }).ToList();
        }
        public async Task<RestFromBusiness> DeleteProduct(Guid productID, Guid LoggedInUser)
        {
            return await _productComponents.DeleteProduct(productID, LoggedInUser);
        }
        public async Task<RestFromBusiness> CreateProduct(Medicine Medicine, Guid LoggedInUser)
        {
            return await _productComponents.CreateProduct(Medicine, LoggedInUser);
        }
        public async Task<RestFromBusiness> UpdateProduct(Medicine Medicine, Guid LoggedInUser)
        {
            return await _productComponents.UpdateProduct(Medicine, LoggedInUser);
        }
        public async Task<Medicine> GetProduct(Guid productID)
        {
            Medicine product = await _productComponents.GetProduct(productID);
            return product;
        }
        public async Task<RestFromBusiness> AddStock(Stock Stock, Guid LoggedInUser)
        {
            return await _productComponents.AddStock(Stock, LoggedInUser);
        }
        public async Task<RestFromBusiness> AddPriceList(PriceList PriceList, Guid LoggedInUser)
        {

            return await _productComponents.AddPriceList(PriceList, LoggedInUser);
        }

    }
}
