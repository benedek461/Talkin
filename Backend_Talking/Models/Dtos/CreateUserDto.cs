using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.Dtos
{
    public class CreateUserDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Birthday { get; set; }
        [Required]
        public string Sex { get; set; }
    }
}
