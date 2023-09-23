using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Credential
    {
        [Required(ErrorMessage = "Kullanıcı adı boş bırakılamaz.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Şifre boş bırakılamaz.")]
        public string Password { get; set; }
    }
}
