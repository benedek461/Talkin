using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkin.Assets.MVVM.Models
{
    public class Conversation
    {
        public int id { get; set; }
        public List<int> partitioners { get; set; }
        public List<ConversationUser> ConversationUsers { get; set; }
        public List<Message> messages { get; set; }
    }
}
