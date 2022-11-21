using System.ComponentModel.DataAnnotations;
using System;

namespace AuthenticationServer.API.Models.Domain_Objects
{
    public class User
    {
        [Key]
        public Guid user_ID { get; set; }
        public string userName { get; set; }
        public string PasswordHash { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
    }
}
