using KekStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class getUser
    {
        public int Id { get; set; }

        public string SecondName { get; set; }

        public string FirstName { get; set; }

        public string urlPhoto { get; set; }

        public string RegistrationDate { get; set; }

        public string LastLogInDate { get; set; }

        public List<Project> projects { get; set; }

        public List<Project> followedProjects { get; set; }
    }
}