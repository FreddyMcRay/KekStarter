using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class Commentary
    {
        public int ProjectId { get; set; }

        public int UserPofileId { get; set; }

        public string DateCreated { get; set; }

        public string Content { get; set; }
    }
}
