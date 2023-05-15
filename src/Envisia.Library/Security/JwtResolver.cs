using Envisia.Library.Security.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Envisia.Library.Security
{
    public class JwtResolver
    {
        private readonly IConfiguration _configuration;

        public JwtResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenValidationParameters GetTokenParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Auth:ValidIssuer"],
                ValidAudience = _configuration["Auth:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Secret"]))
            };
        }

        public string GenerateAccessToken(UserClaims userClaims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Auth:Secret"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new (ClaimConstants.UserId, userClaims.Id),
                new (ClaimConstants.UserName, userClaims.Name)
            };

            foreach (var userRole in userClaims.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["Auth:ValidIssuer"],
                audience: _configuration["Auth:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: signinCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        public AuthRefreshToken GenerateRefreshToken()
        {
            var refreshToken = new AuthRefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(2),
                Created = DateTime.Now
            };

            return refreshToken;
        }
    }
}
