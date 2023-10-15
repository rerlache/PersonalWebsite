using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Helpers
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetToken(string username)
        {
            return GenerateToken(username);
        }

        private string GenerateToken(string username)
        {
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, username)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JWTKey"]));

            SigningCredentials cred = new(key, SecurityAlgorithms.HmacSha256);

            DateTime expire = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWTExpire"]));

            JwtSecurityToken jwtToken = new(
                _configuration["JWTIssuer"],
                _configuration["JWTIssuer"],
                claims,
                expires: expire,
                signingCredentials: cred
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
