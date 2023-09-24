namespace API.Services.GeneralService
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserById(int id);
        Task<UserSecurityQuestion> GetSecurityQuestionForUser(int userId);
        Task<User> AddUser(User user, UserSecurityQuestion question);
        Task<List<User>> UpdateUser(int id, User user);
        Task<List<User>> DeleteUser(int id);
    }
}
