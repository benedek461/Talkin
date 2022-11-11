using System.ComponentModel.DataAnnotations;

namespace TalkinWebAPI.Models
{
    public class Message
    {
        [Key]
        public Guid message_ID { get; set; }
        public User? sender_ID { get; set; }
        public User? receiver_ID { get; set; }
        public string message_text { get; set; }
    }
}
