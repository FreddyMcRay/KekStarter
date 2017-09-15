using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class ResponseUserInfo
    {
        public int Id{ get; set; }

        public string Login { get; set; }

        public string Role { get; set; }

        public string Color { get; set; }

        public string Language { get; set; }

        public string Token { get; set; }
    }
}
