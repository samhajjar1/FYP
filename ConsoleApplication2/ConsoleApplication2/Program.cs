using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facebook;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace ConsoleApplication2
{

    class Program
    {
        private FacebookClient myfbclient;
        private string JavistaPage = "/248586058548523";

        public Program ()
        {
            string AppId = "389520154761799";
            string AppSecret = "e4668b50fc17094e8b88f30774ad11e1";
 
            var client = new WebClient();
            string oauthUrl = string.Format("https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={0}&client_secret={1}", AppId, AppSecret);

            try
            {
                string accessToken = client.DownloadString(oauthUrl).Split('=')[1];
                myfbclient = new FacebookClient(accessToken);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            
        }

        //public FacebookLikes getPostLikes(FacebookPost post)
        //{
        //    var fbData = myfbclient.Get(post.id + "/likes").ToString();



        //}

        //public FacebookComment getPostComments(FacebookPost post)
        //{

        //}

        public FacebookPageInfo getPageInfo(string pageId)
        {
            var pageData = myfbclient.Get(pageId + FacebookPageInfo.fields).ToString();
            var info = JsonConvert.DeserializeObject<FacebookPageInfo>(pageData);
            return info;
        }

        public List<FacebookPost> getAllFBPostsForPage(string pageId)
        {
           var fbData = myfbclient.Get(pageId+ "/posts"+FacebookPost.fields).ToString();
            FacebookPostData posts = null ;
            List<FacebookPost> postsLists = new List<FacebookPost>();
            do
            {
                if (posts != null && posts.Data.Count != 0)
                {
                    fbData = myfbclient.Get(posts.Paging.Next).ToString();

                }
                posts = JsonConvert.DeserializeObject<FacebookPostData>(fbData);
                postsLists.AddRange(posts.Data);
            }
            while (posts.Data.Count != 0);
            return postsLists;
        }

        static void Main(string[] args)
        {
            Program myProgram = new Program();
            FacebookPageInfo pageInfo = myProgram.getPageInfo(myProgram.JavistaPage);
            List<FacebookPost> postsLists = myProgram.getAllFBPostsForPage(myProgram.JavistaPage);
            
        }
    }
}
