using Microsoft.AspNetCore.Mvc;

namespace API.Data.General
{
    public class RegisterDTO
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName { get; set; }
        public string eMail { get; set; }
        public string password { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
    }
}
