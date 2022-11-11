namespace TalkinWebAPI.Requests
{
    public class RegistrationDTO
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string sex { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string dateOfBirth { get; set; } = string.Empty;
    }
}
