﻿namespace ChatAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
