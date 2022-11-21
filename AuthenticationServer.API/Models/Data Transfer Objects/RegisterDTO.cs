using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.API.Models.Data_Transfer_Objects
{
    public class RegisterDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string sex { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string dateOfBirth { get; set; }
    }
}
