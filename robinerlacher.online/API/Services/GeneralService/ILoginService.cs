namespace API.Services.GeneralService
{
    public interface ILoginService
    {
        Task<List<UserLoginHistory>> GetLoginHistoryAsync(int userId);
        Task<Tuple<string, User>> WithDataAsync(string userName, string password, string ip);
        Task<Tuple<string, User>> WithTokenAsync(string token, string ip);
    }
}
