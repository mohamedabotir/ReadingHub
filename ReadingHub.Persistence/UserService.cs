using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
 using ReadingHub.Unit.Abstracts;
using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ReadingHub.Cores.Services
{
    public class UserService :IUserService
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        public UserService(IHttpContextAccessor accessor)
        {
            httpContextAccessor = accessor;
        }

        public string GetUserName() => httpContextAccessor.HttpContext.
            User.Claims.First(c=>c.Type== "Name")
            .Value;

        public string GetEmail() => httpContextAccessor.HttpContext.
            User.Claims.First(c => c.Type == "Email")
            .Value;


        public string GetUserId() => httpContextAccessor.HttpContext.
            User.Claims.First(c => c.Type == "userId")
            .Value;


        public static string GenerateToken(string userId,string userName,string Email,IConfiguration config) {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config["SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim(JwtRegisteredClaimNames.Iat,
                DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),

                    new Claim("userId", userId),
                    new Claim("Name",userName),
                    new Claim("Email",Email),
                     
                }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        
        }
    }
}
