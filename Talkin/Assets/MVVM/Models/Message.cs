using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkin.Assets.MVVM.Models
{
    public class Message
    {
        /*
        public int message_ID { get; set; }
        public List<int> partitioners { get; set; }
        public List<string> message_text { get; set; }
        */
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
