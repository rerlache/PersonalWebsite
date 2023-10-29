using System.Collections;

namespace API.Data.General
{
    public class UserDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public Application[] Apps { get; set; }
        public UserLoginHistoryDTO LastLogin {  get; set; }
        public UserLoginHistoryDTO[] LoginHistory { get; set; }
    }
}
