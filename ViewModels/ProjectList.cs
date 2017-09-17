using KekStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class ProjectList
    {
        public List<Project> SuccessfulProjects { get; set; }

        public List<Project> NewProjects { get; set; }

        public List<String> Tags { get; set; }
    }
}
