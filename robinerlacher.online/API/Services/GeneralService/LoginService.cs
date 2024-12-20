﻿using API.Data;
using API.Data.General;
using API.Helpers;
using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Services.GeneralService
{
    public class LoginService : ILoginService
    {
        private readonly GeneralContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IConfiguration _config;
        private readonly IKeyStoreService _keyStoreService;

        public LoginService(GeneralContext context, IMapper mapper, ITokenGenerator tokenGenerator, IConfiguration config, IKeyStoreService keyStoreService)
        {
            _context = context;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _config = config;
            _keyStoreService = keyStoreService;
        }

        private async Task LogHistory(User user, bool isPassCorrect, string ip)
        {
            UserLoginHistory history = new UserLoginHistory();
            history.LoginDate = DateTime.Now;
            history.UserId = user.ID;
            history.Success = isPassCorrect;
            history.IPAddress = ip;
            _context.UserLoginHistory.Add(history);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserLoginHistoryDTO>> GetLoginHistoryAsync(string userName)
        {
            User? user = await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return new List<UserLoginHistoryDTO>();
            }
            int userId = user.ID;
            return _mapper.Map<List<UserLoginHistoryDTO>>(await _context.UserLoginHistory.Where(i => i.UserId == userId).OrderByDescending(h => h.LoginDate).ToListAsync());
        }

        public async Task<Tuple<string, UserDTO>> WithDataAsync(LoginDTO login, string ip)
        {
            User? user = await _context.Users.Include(u => u.AssignedApps).Where(u => u.UserName == login.userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return new Tuple<string, UserDTO>($"{login.userName} not found", null);
            }
            string passFromDb = user.Password;
            string saltedPass = user.RegisterDate + login.password;
            bool isPassCorrect = BCrypt.Net.BCrypt.Verify(saltedPass, passFromDb);
            await LogHistory(user, isPassCorrect, ip);
            if (!isPassCorrect)
            {
                return new Tuple<string, UserDTO>($"password for {user.UserName} incorrect", null);
            }
            string token = _tokenGenerator.GetToken(login.userName);
            UserDTO returnUser = _mapper.Map<UserDTO>(user);
            returnUser.AccessToken = token;
            return new Tuple<string, UserDTO>(token, returnUser); // TODO: return a token here.
        }

        public async Task<UserDTO> WithTokenAsync(string token, string ip)
        {
            UserDTO result = new();
            JwtSecurityTokenHandler handler = new();
            var temp = _config["JWT:Key"];
            var key = Encoding.UTF8.GetBytes(_config["JWT:Key"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config["JWT:Issuer"],
                ValidAudience = _config["JWT:Audience"],
                IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
                {
                    // Resolve the signing key based on the 'kid'
                    return new List<SecurityKey> { _keyStoreService.GetKey(kid) };
                },
                IssuerSigningKey = (new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]))),
                ClockSkew = TimeSpan.Zero
            };

            try
            {
                var claims = handler.ValidateToken(token, tokenValidationParameters, out var tokenSecure);
                string userName = ((JwtSecurityToken)tokenSecure).Subject;
                User? user = await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

                if (user == null)
                {
                    return result;
                }
                await LogHistory(user, true, ip);
                result = _mapper.Map<UserDTO>(user);
            }
            catch (SecurityTokenException ex)
            {
            }

            return result;
        }
    }
}
