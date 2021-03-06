using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Entities;
using ColoredLive.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ColoredLive.BL.Realizations
{
    [Buisness]
    public class TokenCreationBl : ITokenCreationBl
    {
        private AppSettings _settings;

        public TokenCreationBl(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public string Generate(UserEntity user) => GenerateToken(user.Id, false.ToString());
       

        public string Generate(PartnerEntity partnerEntity) => GenerateToken(partnerEntity.Id, true.ToString());
            

        private string GenerateToken(Guid id, string type)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim("id", id.ToString()),
                        new Claim("isPartner", type)
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
