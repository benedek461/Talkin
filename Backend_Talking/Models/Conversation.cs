namespace ChatAPI.Models
{
    public class Conversation
    {
        public int Id { get; set; }

        public List<User> Partitioners { get; set; }
        public List<ConversationUser> ConversationUsers { get; set; }

        public List<Message> Messages { get; set; }
    }
}
