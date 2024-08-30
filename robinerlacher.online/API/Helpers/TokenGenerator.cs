using API.Services.GeneralService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Helpers
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly IKeyStoreService _keyStoreService;

        public TokenGenerator(IConfiguration configuration, IKeyStoreService keyStoreService)
        {
            _configuration = configuration;
            _keyStoreService = keyStoreService;
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

            string keyString = _configuration["JWTKey"];
            byte[] keyBytes = Encoding.UTF8.GetBytes(keyString);

            if (keyBytes.Length < 32)
            {
                keyBytes = new byte[32];
                RandomNumberGenerator.Fill(keyBytes);
            }

            SymmetricSecurityKey key = new(keyBytes);
            
            SigningCredentials cred = new(key, SecurityAlgorithms.HmacSha256);

            DateTime expire = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JWTExpire"]));

            JwtHeader header = new(cred);
            header["kid"] = Guid.NewGuid().ToString();

            JwtPayload payload = new(
                _configuration["JWTIssuer"],
                _configuration["JWTIssuer"],
                claims,
                notBefore: DateTime.Now,
                expires: expire
                );

            JwtSecurityToken jwtToken = new(header, payload);
            _keyStoreService.StoreKey(header.Kid, key);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
