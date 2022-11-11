using System.ComponentModel.DataAnnotations;

namespace TalkinWebAPI.Models
{
    public class Conversation
    {
        [Key]
        public Guid conversation_ID { get; set; }
        public User? user1_ID { get; set; }
        public User? user2_ID { get; set; }
        public Message? msg_ID { get; set; }
    }
}
