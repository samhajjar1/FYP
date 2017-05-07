using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class Employee
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string screen_name { get; set; }
        public int fixScore { get; set; }
        public int variableScore { get; set; }
        public List<Activity> activities { get; set; }

    }
}