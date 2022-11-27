using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Talkin.Assets.MVVM.Models
{
    public class Token
    {
        public static string CurrentUserJWTToken { get; set; }
    }
}
