using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactManager.Models
{
    public class Activity
    {
        public string platform { get; set; }
        public string action { get; set; }
        public string parentId { get; set; }
        public int points { get; set; }
        public DateTime parentCreatedAt { get; set; }
    }
}