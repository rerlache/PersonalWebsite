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
        Task<string> GetSecurityQuestionForUserAsync(PasswordResetDTO passwordResetDTO);
        Task<UserDTO> AddAsync(User user, UserSecurityQuestion question);
        Task<UserDTO> UpdateAsync(int id, User user);
        Task<List<UserDTO>> DeleteAsync(int id);
        Task<UserDTO> AssignAppToUser(int appId, int userId);
        Task<bool> CheckAnswerToQuestion(int userId, PasswordResetDTO passwordResetDTO);
        Task<UserDTO> ResetPassword(PasswordResetDTO passwordReset);
        bool UsernameAlreadyUsed(string userName);
        bool EmailAlreadyUsed(string email);
    }
}
