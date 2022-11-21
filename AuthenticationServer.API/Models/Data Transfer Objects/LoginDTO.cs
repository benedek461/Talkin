using System.ComponentModel.DataAnnotations;

namespace AuthenticationServer.API.Models.Data_Transfer_Objects
{
    public class LoginDTO
    {
        [Required]
        public string userName { get; set; }
        [Required]
        public string password { get; set; }
    }
}
