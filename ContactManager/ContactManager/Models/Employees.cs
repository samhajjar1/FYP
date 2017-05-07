using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ContactManager.Models
{
    public class Employees
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; }

        //[JsonProperty(PropertyName = "employees")]
        public List<Employee>employees { get; set; }
    }
}