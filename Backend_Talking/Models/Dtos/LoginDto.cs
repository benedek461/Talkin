﻿using System.ComponentModel.DataAnnotations;

namespace ChatAPI.Models.Dtos
{
    public class LoginDto
    {
        /*
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        */
        [Required]
        public string userName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
