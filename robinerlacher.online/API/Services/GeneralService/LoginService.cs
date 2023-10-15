using API.Data;

namespace API.Services.GeneralService
{
    public class LoginService : ILoginService
    {
        private readonly GeneralContext _context;

        public LoginService(GeneralContext context)
        {
            _context = context;
        }

        public async Task<List<UserLoginHistory>> GetLoginHistoryAsync(int userId)
        {
            return await _context.UserLoginHistory.Where(i => i.UserId == userId).OrderByDescending(h => h.LoginDate).ToListAsync();
        }

        public async Task<Tuple<string, User>> WithDataAsync(string userName, string password, string ip)
        {
            User user = await _context.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return new Tuple<string, User>($"{userName} not found", null);
            }
            string passFromDb = user.Password;
            string saltedPass = user.RegisterDate + password;
            bool isPassCorrect = BCrypt.Net.BCrypt.Verify(saltedPass, passFromDb);
            UserLoginHistory history = new UserLoginHistory();
            history.LoginDate = DateTime.Now;
            history.UserId = user.ID;
            history.Success = isPassCorrect;
            history.IPAddress = ip;
            _context.UserLoginHistory.Add(history);
            await _context.SaveChangesAsync();
            if (!history.Success)
            {
                return new Tuple<string, User>($"password for {user.UserName} incorrect", null);
            }
            return new Tuple<string, User>($"welcome {user.Displayname}.", user); // TODO: return a token here.
        }

        public Task<Tuple<string, User>> WithTokenAsync(string token, string ip)
        {
            throw new NotImplementedException();
        }
    }
}
