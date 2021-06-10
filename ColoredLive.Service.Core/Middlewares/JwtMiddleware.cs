using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColoredLive.BL.Interfaces;
using ColoredLive.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ColoredLive.Service.Core.Middlewares
{
    public class JwtMiddleware
    {
        private RequestDelegate _next;
        private AppSettings _settings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> settings)
        {
            _next = next;
            _settings = settings.Value;
        }

        public async Task Invoke(HttpContext context, IUserBl userBl)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                AttachUserToContext(context, userBl, token);
            }

            await _next(context);
        }

        private void AttachUserToContext(HttpContext context, IUserBl userBl, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_settings.SecretKey);
                
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                }, out var validatedToken);

                var jwt = (JwtSecurityToken)validatedToken;
                var userId = Guid.Parse(jwt.Claims.First(x => x.Type == "id").Value);
                
                context.Items["User"] = userBl.GetUser(userId);
            }
            catch
            {

            }
        }
    }
}
