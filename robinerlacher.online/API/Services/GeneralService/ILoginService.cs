using API.Data.General;

namespace API.Services.GeneralService
{
    public interface ILoginService
    {
        Task<List<UserLoginHistoryDTO>> GetLoginHistoryAsync(string userName);
        Task<Tuple<string, UserDTO>> WithDataAsync(LoginDTO login, string ip);
        Task<UserDTO> WithTokenAsync(string token, string ip);
    }
}
