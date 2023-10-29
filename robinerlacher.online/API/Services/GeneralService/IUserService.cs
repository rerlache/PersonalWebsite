using API.Data.General;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Services.GeneralService
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(int id);
        Task<int> GetUserIdByUsername(string username);
        Task<UserSecurityQuestion> GetSecurityQuestionForUserAsync(int userId);
        Task<UserDTO> AddAsync(User user, UserSecurityQuestion question);
        Task<UserDTO> UpdateAsync(int id, User user);
        Task<List<UserDTO>> DeleteAsync(int id);
        bool UsernameAlreadyUsed(string userName);
        bool EmailAlreadyUsed(string email);
    }
}
