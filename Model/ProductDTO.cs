using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductDTO
    {
        public Guid MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string UniqueCode { get; set; }
        public decimal? ListPrice { get; set; }
        public int StockAmount { get; set; }
    }
}
