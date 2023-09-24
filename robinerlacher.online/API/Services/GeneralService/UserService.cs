using API.Data;

namespace API.Services.GeneralService
{
    public class UserService : IUserService
    {
        private readonly GeneralContext _context;

        public UserService(GeneralContext context)
        {
            _context = context;
        }
        public async Task<User> AddUser(User user, UserSecurityQuestion question)
        {
            bool idExists = await _context.Users.FindAsync(user.ID) != null;
            if (idExists)
            {
                return null;
            }
            _context.Users.Add(user);
            question.User = user;
            _context.UserSecurityQuestions.Add(question);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> DeleteUser(int id)
        {
            User user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return _context.Users.ToList();
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.Include(u => u.SecurityQuestion).Include(a => a.AssignedApps).ToListAsync();
        }

        public async Task<UserSecurityQuestion> GetSecurityQuestionForUser(int userId)
        {
            UserSecurityQuestion question = await _context.UserSecurityQuestions.Where(q => q.UserID == userId).FirstAsync();
            return question;
        }

        public async Task<User> GetUserById(int id)
        {
            User user = await _context.Users.FindAsync(id);
            user.SecurityQuestion = await _context.UserSecurityQuestions.Where(u => u.UserID == user.ID).FirstAsync();
            return user;
        }

        public async Task<List<User>> UpdateUser(int id, User user)
        {
            User updatedUser = await _context.Users.FindAsync(id);
            if (updatedUser == null)
            {
                return null;
            }
            updatedUser.FirstName = updatedUser.FirstName != user.FirstName ? user.FirstName : updatedUser.FirstName;
            updatedUser.LastName = updatedUser.LastName != user.LastName ? user.LastName : updatedUser.LastName;
            updatedUser.UserName = updatedUser.UserName != user.UserName ? user.UserName : updatedUser.UserName;
            updatedUser.Password = updatedUser.Password != user.Password ? user.Password : updatedUser.Password;
            updatedUser.Email = updatedUser.Email != user.Email ? user.Email : updatedUser.Email;

            await _context.SaveChangesAsync();

            return await _context.Users.ToListAsync();
        }
    }
}
