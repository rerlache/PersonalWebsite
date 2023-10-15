using API.Data.General;

namespace API.Services.GeneralService
{
    public interface ILoginService
    {
        Task<List<UserLoginHistoryDTO>> GetLoginHistoryAsync(int userId);
        Task<Tuple<string, UserDTO>> WithDataAsync(string userName, string password, string ip);
        Task<UserDTO> WithTokenAsync(string token, string ip);
    }
}
