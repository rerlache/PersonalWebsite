using API.Data;
using API.Data.General;
using AutoMapper;

namespace API.Services.GeneralService
{
    public class UserService : IUserService
    {
        private readonly GeneralContext _context;
        private readonly IMapper _mapper;

        public UserService(GeneralContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #region Helper Methods
        public string GetHashedPassword(DateTime regDate, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(regDate + password);
        }
        #endregion

        #region API Call Methods
        public async Task<UserDTO> AddAsync(User user, UserSecurityQuestion question)
        {
            bool idExists = await _context.Users.FindAsync(user.ID) != null;
            if (idExists)
            {
                return null;
            }
            user.Password = GetHashedPassword(user.RegisterDate, user.Password);
            _context.Users.Add(user);
            question.User = user;
            _context.UserSecurityQuestions.Add(question);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<List<UserDTO>> DeleteAsync(int id)
        {
            User user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<List<UserDTO>>(_context.Users.ToList());
        }

        public async Task<List<UserDTO>> GetAllAsync()
        {
            return _mapper.Map<List<UserDTO>>(await _context.Users.Include(u => u.SecurityQuestion).Include(a => a.AssignedApps).ToListAsync());
        }

        public async Task<UserSecurityQuestion> GetSecurityQuestionForUserAsync(int userId)
        {
            UserSecurityQuestion question = await _context.UserSecurityQuestions.Where(q => q.UserID == userId).FirstAsync();
            return question;
        }

        public async Task<UserDTO> GetByIdAsync(int id)
        {
            return _mapper.Map<UserDTO>(await _context.Users.Include(u => u.SecurityQuestion).FirstOrDefaultAsync(u => u.ID == id));
        }

        public async Task<UserDTO> UpdateAsync(int id, User user)
        {
            User updatedUser = await _context.Users.FindAsync(id);
            if (updatedUser == null)
            {
                return null;
            }
            updatedUser.FirstName = updatedUser.FirstName != user.FirstName ? user.FirstName : updatedUser.FirstName;
            updatedUser.LastName = updatedUser.LastName != user.LastName ? user.LastName : updatedUser.LastName;
            updatedUser.Email = updatedUser.Email != user.Email ? user.Email : updatedUser.Email;

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDTO>(updatedUser);
        }

        public bool UsernameAlreadyUsed(string userName)
        {
            User user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            return user != null;
        }
        public bool EmailAlreadyUsed(string email)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user != null;
        }

        public async Task<int> GetUserIdByUsername(string username)
        {
            return _context.Users.Where(u => u.UserName == username).FirstOrDefaultAsync().Id;
        }

        public async Task<UserDTO> AssignAppToUser(int appId, int userId)
        {
            Application? app = await _context.Applications.FindAsync(appId);
            User? user = await _context.Users.FindAsync(userId);
            if (app == null || user == null)
            {
                return new();
            }
            user.AssignedApps?.Add(app);
            app.Users?.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }

        #endregion


    }
}
