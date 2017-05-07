using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Core;
using Tweetinvi.Core.Credentials;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Core.Interfaces;
using Tweetinvi.Core.Interfaces.Streaminvi;
using Tweetinvi.Core.Parameters;
using Tweetinvi.Json;
using Tweetinvi.Models;
using Tweetinvi.Models.DTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ConsoleApplication1
{
    class Program
    {
        public void tests()//to ignore
        {
            var user = User.GetAuthenticatedUser();
            var myFriends = user.GetFriends();

            var tweets = Search.SearchTweets("Javista_CRM");
            var timeline = Timeline.GetHomeTimeline();//my logged in timeine

            var searchparam = new Tweetinvi.Parameters.SearchTweetsParameters("from:Javista_CRM exclude:replies")
            {
                MaximumNumberOfResults = 300,
                TweetSearchType = Tweetinvi.Parameters.TweetSearchType.OriginalTweetsOnly,
                SearchType = SearchResultType.Recent,
                Until = new DateTime(2017, 04, 12),
                Since = new DateTime(2017, 04, 09),
                //Filters = Tweetinvi.Parameters.TweetSearchFilters.Replies,
            };
            var tweetss = Search.SearchTweets(searchparam);

            var users = Search.SearchUsers("Javista_CRM");

            var javistaFollowers = users.ToList()[0].FollowersCount;
            var javistaF = users.ToList()[0].GetFollowers();


            //var query = $"https://api.twitter.com/1.1/favorites/list.json?count={maxNumberOfTweets}&user_id={lu.Id}";
            ////GET https://api.twitter.com/1.1/search/tweets.json?q=%23freebandnames&since_id=24012619984051000&max_id=250126199840518145&result_type=mixed&count=4
            //var favoriteDTOs = TwitterAccessor.ExecuteGETQuery<IEnumerable<ITweetDTO>>(query);
            //var favorites = Tweet.GenerateTweetsFromDTO(favoriteDTOs);
        }

        public IUser getUserPage(string screenName)
        {
            var javiPage = User.GetUserFromScreenName(screenName);
            return javiPage;
        }

        public ITweet[] getJaviTweetsByID(int javistaID)
        {
            var lastTweets = Timeline.GetUserTimeline(javistaID, 50).ToArray();
            return lastTweets;
        }

        public ITweet[] getJaviTweetsByPage(IUser userPage)
        {
            var javistaID = userPage.Id;
            var lastTweets = Timeline.GetUserTimeline(javistaID, 50).ToArray();
            return lastTweets;
        }
        public IEnumerable<ITweet> getJaviTweetsByTimeInterval(IUser userPage)
        {
            var userTimelineParameters = Timeline.CreateUserTimelineParameter();
            userTimelineParameters.IncludeRTS = true;
            userTimelineParameters.MaximumNumberOfTweetsToRetrieve = 35;
            userTimelineParameters.SinceId = 21;
            userTimelineParameters.ExcludeReplies = true;
            IEnumerable<ITweet> tweetsers = Timeline.GetUserTimeline(userPage.Id, userTimelineParameters);

            //var x = tweetsers.First().FavoriteCount;



            return tweetsers;
        }

        public IEnumerable<ITweet> getTweetsSinceDate(IUser userPage, DateTime date)
        {
            //var searchparam = new Tweetinvi.Parameters.SearchTweetsParameters("from:" + userPage.ScreenName + " exclude:replies")
            var searchparam = new Tweetinvi.Parameters.SearchTweetsParameters("from:" + userPage.ScreenName + " exclude:replies" + " test")
            {
                MaximumNumberOfResults = 300,
                TweetSearchType = Tweetinvi.Parameters.TweetSearchType.OriginalTweetsOnly,
                SearchType = SearchResultType.Recent,
                //Until = new DateTime(2017, 04, 12),
                //Since = new DateTime(2017, 04, 09),
                Since = date,
            };
            var tweetss = Search.SearchTweets(searchparam);
            return tweetss;
        }

        public ITweet[] getJaviOriginalTweets(IUser userPage)
        {
            var userTimelineParameters = Timeline.CreateUserTimelineParameter();
            userTimelineParameters.IncludeRTS = false;
            var tweets = Timeline.GetUserTimeline(userPage.Id, userTimelineParameters);
            return tweets.ToArray();
        }

        public Hashtable getRetweetedTweets(IUser javiPage)
        {
            var tweets = Timeline.GetUserTimeline(javiPage.Id, 10).ToArray();
            //var retweets = new List<ITweet>();
            Hashtable retweetsHash = new Hashtable();
            foreach (Tweetinvi.Logic.Tweet tweet in tweets)
            {
                if (!tweet.IsRetweet && tweet.RetweetCount > 0)
                {
                    //Console.WriteLine(tweet.RetweetCount);
                    retweetsHash.Add(tweet.Id, tweet.GetRetweets());
                    //retweets.Add(tweet);
                }
            }
            //return retweets;
            return retweetsHash;
        }

        public List<string> getRetweetersByTweetID(long id, Hashtable retweets)
        {
            if (retweets.ContainsKey(id))
            {
                List<ITweet> retweeters = (List<ITweet>)retweets[id];
                List<string> users = new List<string>();
                Console.WriteLine(retweeters[0].Text);
                foreach (ITweet user in retweeters)
                {
                    Console.WriteLine(user.CreatedBy);
                    users.Add(user.CreatedBy.ToString());
                }
                return users;
            }
            return null;
        }

        public int getFollowersCount(IUser userPage)
        {
            return userPage.FollowersCount;
        }

        public IEnumerable<IUser> getFollowers(IUser userPage)
        {
            return userPage.GetFollowers();
        }

        public void search(string query)
        {
            //var param = Tweetinvi.Parameters.TweetSearchFilters.Replies;


            var searchParameter = new Tweetinvi.Parameters.SearchTweetsParameters(query)
            {
                MaximumNumberOfResults = 200,
                Since = new DateTime(2016, 3, 12),
                //Filters = RepliesFilterType.RepliesToKnownUsers,
            };
            var tweets = Search.SearchTweets(searchParameter);



            var x = Search.SearchTweets("from:Javista_CRM");
        }

        public ITweet getTweetInfo(long id)
        {
            var tweet = Tweet.GetTweet(851785389757132800);
            return tweet;
        }

        public void javistaInfo()//to ignore
        {
            //get Javista's page
            var javiPage = Search.SearchUsers("Javista").ToList()[2];

            //get javista's tweets
            var javistaID = javiPage.Id;
            var lastTweets = Timeline.GetUserTimeline(javistaID, 50).ToArray();


            //get tweets containing keyword
            foreach (Tweetinvi.Logic.Tweet tweet in lastTweets)
            {
                if (tweet.ToString().Contains("MSDYN365"))
                {
                    Console.WriteLine(tweet);
                }
            }

            //get the retweeted Tweets
            foreach (Tweetinvi.Logic.Tweet tweet in lastTweets)
            {
                if (!tweet.IsRetweet && tweet.RetweetCount > 0)
                {
                    Console.WriteLine(tweet.RetweetCount);
                    Console.WriteLine(tweet.ToJson());
                }
            }


            //autre methode nope
            //var searchparam = new Tweetinvi.Parameters.SearchTweetsParameters(Timeline.GetUserTimeline(javistaID, 30).ToString())
            //{
            //    MaximumNumberOfResults = 30,
            //    //Since = new DateTime(2017, 01, 10),
            //    //Until = new DateTime(2017, 03, 10),
            //    //TweetSearchType = Tweetinvi.Parameters.TweetSearchType.OriginalTweetsOnly,
            //};
            //var tweetss = Search.SearchTweets(searchparam);


            //Autre methode to get the original tweets
            var userTimelineParameters = Timeline.CreateUserTimelineParameter();
            userTimelineParameters.IncludeRTS = false;
            var tweetsers = Timeline.GetUserTimeline(javistaID, userTimelineParameters);


            //get retweets of me
            //var javiRetweets = javiPage.Timeline.GetEnumerator();
            var x = javiPage.TweetsRetweetedByFollowers;


            //var javiTags = Search.SearchTweets("#Javista");
            //var javiEntities = javiPage.Entities.Description.ToString();

            //var javiFollowers = javiPage.Followers;
            //var javiFriends = javiPage.Friends;
            //var javiFriendsRetweet = javiPage.FriendsRetweets;
            //var javiRetweets = javiPage.Retweets;

            //var javiGetFavorites = javiPage.GetFavorites();
            //var javiGetFollowers = javiPage.GetFollowers();
            //var javiGetFriends = javiPage.GetFriends();
            //var javiGetTimeline = javiPage.GetUserTimeline();
            //var javiTimeline = javiPage.Timeline.ToJson();
            //var javiToJson = javiPage.ToJson();
            //var javiRetweetedByFollowers = javiPage.TweetsRetweetedByFollowers.ToJson();
        }
        static void Main(string[] args)
        {
            Auth.SetUserCredentials("IVVasZNY6Fkdwi32BOVKuuXvG", "6KSfzns39G0Wjsw0LS8P3eayQsIZuFHVEUS3nYeJlIizK1vrhi", "838041203895586816-5GkpOpuLUJq17nYGcHEAZrf6IOI2Dvx", "UFnrldn285PnO8LOPFF5Ms8R5VCd13SfkkWLG4GEiGT8g");

            var program = new Program();
            program.tests();

            var javiPage = program.getUserPage("pointsDfidelite");
            //var javiPage = program.getUserPage("pointsDfidelite");

            var time = program.getTweetsSinceDate(javiPage, new DateTime(2017, 04, 09));


            var javitweets = program.getJaviTweetsByPage(javiPage);
            JObject oo = new JObject();
            oo["TId"] = javitweets[2].Id;
            var obj = new JArray();
            foreach (Tweetinvi.Logic.Tweet retweet in javitweets[2].GetRetweets())
            {




                var arr = new JArray();
                arr.Add(retweet.Id);
                arr.Add(retweet.Text);
                obj.Add(arr);
            }
            oo["retweets"] = JsonConvert.DeserializeObject<JArray>(obj.ToJson());
            Console.WriteLine(oo);

            //program.javistaInfo();
            //List<ITweet> retweets = program.getRetweetedTweets(javiPage);
            Hashtable retweets = program.getRetweetedTweets(javiPage);
            //var users = program.getRetweetersByTweetID(850001426399080448, retweets);
            //var users = program.getRetweetersByTweetID(851775854241828864, retweets);


            var deletedTweetStatus = program.getTweetInfo(852483696733061120);

            //var numFollowers = program.getFollowersCount(javiPage);
            //var Followers = program.getFollowers(javiPage);

            //program.search("from:Javista_CRM");
            //var tweetsByTime = program.getJaviTweetsByTimeInterval(javiPage);

            var tweetsSince = program.getTweetsSinceDate(javiPage, new DateTime(2017,4,15));
        }
    }
}
