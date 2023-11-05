using System.ComponentModel.DataAnnotations;

namespace API.Data.General
{
    public class PasswordResetDTO
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Answer { get; set; }
        public string? Password { get; set; }
    }
}
