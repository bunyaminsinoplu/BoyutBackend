using Components.UserComponents;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Model.Enums;

namespace Components.ProductComponents
{
    public class ProductComponents : IProductComponents
    {
        private readonly BoyutCaseContext _context;

        public ProductComponents(BoyutCaseContext context)
        {
            _context = context;
        }

        public async Task<RestFromBusiness> CreateProduct(Medicine Medicine, Guid LoggedInUser)
        {
            try
            {
                Medicine.Id = Guid.NewGuid();
                Medicine.CreatedDate = DateTime.Now;
                Medicine.UpdatedDate = DateTime.Now;
                Medicine.UpdatedBy = LoggedInUser;
                Medicine.IsDeleted = false;
                await _context.Medicine.AddAsync(Medicine);
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
        public async Task<RestFromBusiness> DeleteProduct(Guid productID, Guid LoggedInUser)
        {
            try
            {
                Medicine medicine = await _context.Medicine.FirstOrDefaultAsync(c => c.Id == productID);
                if (medicine != null)
                {
                    medicine.IsDeleted = true;
                    medicine.UpdatedDate = DateTime.Now;
                    medicine.UpdatedBy = LoggedInUser;
                    _context.Entry(medicine).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return new RestFromBusiness()
                    {
                        Message = "Silme işlemi başarılı",
                        StatusCode = 200,
                    };
                }
                else
                {
                    throw new ApplicationException("Ürün bulunamadı");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<List<Medicine>> GetAllProducts()
        {
            return await _context.Medicine.Where(x => !x.IsDeleted)
                .Include(c => c.PriceList.Where(c => c.IsActive))
                .Include(p => p.Stock.Where(s => !s.IsDeleted))
                .ToListAsync();
        }
        public async Task<Medicine> GetProduct(Guid productID)
        {
            return await _context.Medicine.Where(x => x.Id == productID && !x.IsDeleted)
                .FirstOrDefaultAsync();
        }
        public async Task<RestFromBusiness> UpdateProduct(Medicine Medicine, Guid LoggedInUser)
        {
            try
            {
                Medicine existMedicine = await _context.Medicine.FirstOrDefaultAsync(c => c.Id == Medicine.Id);
                if (existMedicine != null)
                {

                    existMedicine.MedicineName = Medicine.MedicineName;
                    existMedicine.UniqueCode = Medicine.UniqueCode;
                    existMedicine.UpdatedDate = DateTime.Now;
                    existMedicine.UpdatedBy = LoggedInUser;
                    _context.Entry(existMedicine).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return new RestFromBusiness()
                    {
                        Message = "Güncelleme işlemi başarılı",
                        StatusCode = 200,
                    };
                }
                else
                {
                    throw new ApplicationException("Ürün bulunamadı");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
        public async Task<RestFromBusiness> AddStock(Stock Stock, Guid LoggedInUser)
        {
            try
            {
                Stock.Id = Guid.NewGuid();
                Stock.CreatedDate = DateTime.Now;
                Stock.UpdatedDate = DateTime.Now;
                Stock.UpdatedBy = LoggedInUser;
                Stock.IsDeleted = false;
                Stock.Direction = (byte)StockDirections.Purchase;
                await _context.Stock.AddAsync(Stock);
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
        public async Task<RestFromBusiness> AddPriceList(PriceList PriceList, Guid LoggedInUser)
        {
            try
            {
                PriceList existPriceList = await _context.PriceList.FirstOrDefaultAsync(c => c.IsActive && c.MedicineId == PriceList.MedicineId);
                if (existPriceList != null)
                {
                    existPriceList.IsActive = false;
                    existPriceList.UpdatedDate = DateTime.Now;
                    existPriceList.UpdatedBy = LoggedInUser;
                    _context.Entry(existPriceList).State = EntityState.Modified;
                }

                PriceList.Id = Guid.NewGuid();
                PriceList.CreatedDate = DateTime.Now;
                PriceList.UpdatedDate = DateTime.Now;
                PriceList.UpdatedBy = LoggedInUser;
                PriceList.IsActive = true;

                await _context.PriceList.AddAsync(PriceList);
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

    }
}
