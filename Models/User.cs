using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.Models
{
    public class User : IdentityUser
    {
    }

    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string UrlPhoto { get; set; }
        public string Role { get; set; }
        public string RegistrationDate { get; set; }
        public string LastLogInDate { get; set; }
        public string Language { get; set; }
        public string Color { get; set; }
        public User User { get; set; }
        public ICollection<AchivmentUser> Achivments { get; set; }
        public ICollection<Project> Projects { get; set; }

        public UserProfile()
        {
            Achivments = new List<AchivmentUser>();
            Projects = new List<Project>();
        }

    }
}
