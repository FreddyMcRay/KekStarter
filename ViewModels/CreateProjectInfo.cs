﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KekStarter.ViewModels
{
    public class CreateProjectInfo
    {
        public int id { get; set; }

        public string image { get; set; }

        public string title { get; set; }

        public string completionDate { get; set; }

        public string description { get; set; }

        public string content { get; set; }

        public int totalCost { get; set; }

        public List<FinansalGoal> finansalGoals { get; set; }

        public List<string> tags { get; set; }

        public int userId { get; set; }
    }

    public class FinansalGoal
    {
        public string title { get; set; }

        public int cost { get; set; }
    }
}
