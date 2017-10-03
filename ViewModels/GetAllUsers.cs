using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class GetAllUsers
    {
        public int Id { get; set; }

        public bool onCheck { get; set; }

        public bool isBlocked { get; set; }

        public string userName { get; set; }

        public string email { get; set; }

        public string role { get; set; }

        public string image { get; set; }

        public string scanImage { get; set; }
    }
}