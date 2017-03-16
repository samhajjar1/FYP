using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    public class FacebookComment
    {
        //TODO

    }

    public class FacebookPageInfo
    {
        public static string fields = "?fields=id,name,about,awards,category,checkins,description,cover,emails,fan_count,link,location,overall_star_rating,phone,rating_count,single_line_address,talking_about_count,website,were_here_count";
        public long id { get; set; }
        public string name { get; set; }
        public string about { get; set; }
        public string awards { get; set; }
        public string category { get; set; }
        public int checkins { get; set; }
        public string description { get; set; }
        public Cover cover { get; set; }
        public List<string> emails { get; set; }
        public int fan_count { get; set; }
        public string link { get; set; }
         public Location location { get; set; }
        public int overall_star_rating { get; set; }
        public string phone { set; get; }
        public int rating_count { set; get; }
        public string single_line_address { set; get; }
        public int talking_about_count { get; set; }
        public string website { get; set; }
        public int were_here_count { get; set; }
    }

    public class Location
    {
        //todo
    }

    public class Cover
    {
        //todo
    }

    public class FacebookPost
    {

        public static string fields = "?fields=id,message,created_time,link";
        public string message { get; set; }
        public string created_Time { get; set; }
        public string id { get; set; }
        public string link { get; set; }




    }

    public class FacebookPagingInfo
    {
        public string Previous { get; set; }
        public string Next { get; set; }
    }

    public class FacebookPostData
    {
        public List<FacebookPost> Data { get; set; }
        public FacebookPagingInfo Paging { get; set; }
    }



}
