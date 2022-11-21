using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenticationServer.API.Models.Domain_Objects
{
    public class Tokens
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public Guid userRefID { get; set; }
        public string token { get; set; }
    }
}
