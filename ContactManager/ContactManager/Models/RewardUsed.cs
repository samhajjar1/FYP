using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class RewardUsed
    {
        public string name { get; set; }
        public int score { get; set; }
        public DateTime convertDate { get; set; }
        public RewardUsed(string Name, int Score, DateTime time)
        {
            this.name = Name;
            this.score = Score;
            this.convertDate = time;
        }
    }
}