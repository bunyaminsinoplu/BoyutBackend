using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Enums
    {
        public enum StockDirections
        {
            [Display(Name = "Alış")]
            Purchase = 1,
            [Display(Name = "Satış")]
            Sale = 2,
        }
    }
}
