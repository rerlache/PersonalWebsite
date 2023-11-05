using System.Collections;

namespace API.Data.General
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
        public Application[]? Apps { get; set; }
        public UserLoginHistoryDTO[]? LoginHistory { get; set; }
    }
}
