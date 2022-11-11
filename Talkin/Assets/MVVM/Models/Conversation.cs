using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkin.Assets.MVVM.Models
{
    internal class Conversation
    {
        public Guid conversation_ID { get; set; }
        public User user1_ID { get; set; }
        public User user2_ID { get; set; }
        public Message msg_ID { get; set; }
    }
}
