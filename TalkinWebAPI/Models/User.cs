using System.ComponentModel.DataAnnotations;

namespace TalkinWebAPI.Models
{
    public class User
    {
        [Key]
        public Guid user_ID { get; set; }
        public string userName { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
