namespace TalkinWebAPI.Requests
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
    }
}
