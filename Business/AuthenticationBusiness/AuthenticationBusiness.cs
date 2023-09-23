using Business.Helpers;
using Components.UserComponents;
using DataAccess.Data;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business.AuthenticationBusiness
{
    public class AuthenticationBusiness : IAuthenticationBusiness
    {
        private readonly IUserComponents _userComponents;
        private readonly IConfiguration _config;
        public AuthenticationBusiness(IUserComponents userComponents, IConfiguration config)
        {
            _userComponents = userComponents;
            _config = config;
        }

        public async Task<RestFromAuthentication> Login(Credential credential)
        {
            RestFromAuthentication restFromAuthentication = new();
            if (credential.Password != null && credential.Username != null)
            {
                Users user = await _userComponents.GetUserByUserName(credential.Username);
                if (user != null)
                {
                    string EncryptPassword = EncryptionHelper.Encrypt(credential.Password, user.Id.ToString());
                    if (user.Password == EncryptPassword)
                    {
                        restFromAuthentication.StatusCode = 200;
                        restFromAuthentication.Message = "Giriş başarılı";
                        restFromAuthentication.JWTToken = JWTHelper.GenerateJWT(user.Username, user.Name, user.Surname, user.Password, user.UserRole.RoleName, _config["Jwt:Key"], _config["Jwt:Issuer"]);
                        return restFromAuthentication;
                    }
                    else
                    {
                        throw new ApplicationException("Şifre yanlış");
                    }
                }
                else
                {
                    throw new ApplicationException("Sisteme kayıtlı kullanıcı adı bulunamadı");
                }
            }
            else
            {
                throw new ApplicationException("Kullanıcı adı veya şifre boş olamaz");
            }

        }
    }
}
