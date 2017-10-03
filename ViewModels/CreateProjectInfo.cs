using System;
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

        public Payment payment { get; set; }
    }

    public class UpdateGoals
    {
        public int projectId { get; set; }

        public List<FinansalGoal> goals { get; set; }
    }

    //Payment

    public class Payment
    {
        public string cardNumber { get; set; }

        public string expirationDate { get; set; }

        public string cvCode { get; set; }

        public string owner { get; set; }
    }

    public class FinansalGoal
    {
        public bool isCompleted { get; set; }

        public string title { get; set; }

        public int cost { get; set; }
    }
}
