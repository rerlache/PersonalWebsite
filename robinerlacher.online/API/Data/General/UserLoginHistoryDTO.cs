namespace API.Data.General
{
    public class UserLoginHistoryDTO
    {
        public string? IPAddress { get; set; }
        public DateTime LoginDate { get; set; }
        public bool Success { get; set; }
    }
}
