using KekStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class getUser
    {
        public string SecondName { get; set; }

        public string FirstName { get; set; }

        public string UrlPhoto { get; set; }

        public string RegistrationDate { get; set; }

        public string LastLogInDate { get; set; }

        public List<Project> Userprojects { get; set; }
    }
}