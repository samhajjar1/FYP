using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class Rewards
    {
        public string id { get; set; }
        public List<Reward> rewards { get; set; }
    }
}