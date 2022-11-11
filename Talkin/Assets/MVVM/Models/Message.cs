using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkin.Assets.MVVM.Models
{
    internal class Message
    {
        public Guid message_ID { get; set; }
        public User sender_ID { get; set; }
        public User receiver_ID { get; set; }
        public string message_text { get; set; }
    }
}
