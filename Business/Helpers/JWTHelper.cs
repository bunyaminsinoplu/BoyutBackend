using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class JWTHelper
    {
        public static string GenerateJWT(string UserName, string Name, string Surname, string Password, string Role, string JWTKey, string JWTIssuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("UserName", UserName),
                new Claim("Name", Name),
                new Claim("Surname", Surname),
                new Claim("Password", Password),
                new Claim("Role", Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(JWTIssuer,
                JWTKey,
                claims,
                expires: DateTime.Now.AddDays(180),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
