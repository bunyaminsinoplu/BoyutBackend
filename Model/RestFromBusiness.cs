using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class RestFromBusiness
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
    public class RestFromAuthentication : RestFromBusiness
    {
        public string JWTToken { get; set; }
    }
}
