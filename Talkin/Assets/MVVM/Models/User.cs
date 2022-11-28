using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkin.Assets.MVVM.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        //public string Name { get; set; }
        public string Birthday { get; set; }
        public string Sex { get; set; }
        public List<int> ConversationIds{ get; set; }
    }
}
