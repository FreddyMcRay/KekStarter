using KekStarter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class Commentary
    {
        public int Id { get; set; }

        public int projectid { get; set; }

        public int userid { get; set; }

        public UserProfileMini userProfile { get; set; }

        public string dataCreated { get; set; }

        public string content { get; set; }
    }

    public class ResponseComments
    {
        public List<Commentary> commentaryes { get; set; }
    }

    public class RemoveComment
    {
        public int userid { get; set; }

        public int id { get; set; }

        public int projectid { get; set; }

        public int createUser { get; set; }
    }
}
