using ECommerceAPI.Application.Abstractions.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int munite)
        {
            Application.DTOs.Token token = new();

            // Security Keyin simetriğini alıyoruz
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token: SecurityKey"]));

            //Şifrelenmiş kimliği oluşturuyoruz
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarları
            token.Expiration = DateTime.UtcNow.AddMinutes(munite);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token: Audience"],
                issuer: _configuration["Token: Issuer"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials
            );

            //Token oluşturucu sınıfından bir örnek alalım.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            return token;
        }
    }
}
