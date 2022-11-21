using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkin.Assets.MVVM.Models
{
    public class User
    {
        public Guid user_ID { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string sex { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
    }
}
