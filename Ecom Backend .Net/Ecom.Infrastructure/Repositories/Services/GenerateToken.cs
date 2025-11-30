using Ecom.Core.Entities;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories.Services
{
    public class GenerateToken : IGenerateToken
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<AppUser> _userManger;
        public GenerateToken(IOptions<JwtSettings> jwtSettings, UserManager<AppUser> userManger)
        {
            _jwtSettings = jwtSettings.Value;
            _userManger = userManger;
        }
        public async Task<string> GetAndCreateTokenAsync(AppUser user)
        {
            var roles =await _userManger.GetRolesAsync(user);

            List<Claim> claims = new List<Claim>
    {
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim(ClaimTypes.Name, user.UserName!)
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            string secret = _jwtSettings.Secret;

            if (string.IsNullOrEmpty(secret))
            {
                throw new ArgumentNullException("Token secret is not configured");
            }

            byte[] key = Encoding.ASCII.GetBytes(secret);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(30), // Adjust token expiration time as needed
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = credentials,
                NotBefore = DateTime.UtcNow
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenString = tokenHandler.WriteToken(token);

            // Log the token to ensure it looks correct
            Console.WriteLine("Generated JWT: " + tokenString);

            return tokenString;
        }
    }
}
