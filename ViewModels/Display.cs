using KekStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KekStarter.ViewModels
{
    public class Display
    {
        public Project project { get; set; }

        public List<string> tags { get; set; }

        public List<FinansalGoal> finansalGoal { get; set; }

        public UserProfileMini user { get; set; }

    }

    public class UserProfileMini
    {
        public int id { get; set; }

        public string firstName { get; set; }

        public string secondName { get; set; }

        public string urlPhoto { get; set; }
    }
}
