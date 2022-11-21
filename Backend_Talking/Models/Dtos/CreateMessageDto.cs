namespace ChatAPI.Models.Dtos
{
    public class CreateMessageDto
    {
        public int SenderId { get; set; }
        public string Text { get; set; }
        public int ConversationId { get; set; }
    }
}
