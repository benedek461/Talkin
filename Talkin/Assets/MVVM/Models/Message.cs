using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkin.Assets.MVVM.Models
{
    internal class Message
    {
        public int message_ID { get; set; }
        public List<int> partitioners { get; set; }
        public List<string> message_text { get; set; }
    }
}
