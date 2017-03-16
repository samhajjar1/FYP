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

namespace ConsoleApplication1
{
    class Program
    {
        public void tests()//to ignore
        {
            var user = User.GetAuthenticatedUser();
            var myFriends = user.GetFriends();

            var tweets = Search.SearchTweets("#Javista");
            var timeline = Timeline.GetHomeTimeline();

            var searchparam = new Tweetinvi.Parameters.SearchTweetsParameters("Obama")
            {
                MaximumNumberOfResults = 30,
                TweetSearchType = Tweetinvi.Parameters.TweetSearchType.OriginalTweetsOnly
                //SearchType = SearchResultType.Popular,
                //Until = new DateTime(2015, 10, 10),
                //Since = new DateTime(2010, 10, 10)
            };
            var tweetss = Search.SearchTweets(searchparam);

            var users = Search.SearchUsers("Javista");

            var javistaFollowers = users.ToList()[2].FollowersCount;
            var javistaF = users.ToList()[2].GetFollowers();
        }

        public IUser getJavistaPage()
        {
            var javiPage = User.GetUserFromScreenName("Javista_CRM");
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
            //userTimelineParameters.ExcludeReplies = true;
            var tweetsers = Timeline.GetUserTimeline(userPage.Id, userTimelineParameters);
            return tweetsers;
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
            var tweets = Timeline.GetUserTimeline(javiPage.Id, 5).ToArray();
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

        public void javistaInfo()//to ignore
        {
            //get Javista's page
            var javiPage = Search.SearchUsers("Javista").ToList()[2];

            //get javista's tweets
            var javistaID = javiPage.Id;
            var lastTweets = Timeline.GetUserTimeline(javistaID, 50).ToArray();


            ////get tweets containing keyword
            //foreach(Tweetinvi.Logic.Tweet tweet in lastTweets)
            //{
            //    if (tweet.ToString().Contains("MSDYN365"))
            //    {
            //        Console.WriteLine(tweet);
            //    }
            //}

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

            //autre methode pr credentials
            //var creds = new TwitterCredentials("IVVasZNY6Fkdwi32BOVKuuXvG", "6KSfzns39G0Wjsw0LS8P3eayQsIZuFHVEUS3nYeJlIizK1vrhi", "838041203895586816-5GkpOpuLUJq17nYGcHEAZrf6IOI2Dvx", "UFnrldn285PnO8LOPFF5Ms8R5VCd13SfkkWLG4GEiGT8g");
            //var tweet = Auth.ExecuteOperationWithCredentials(creds, () =>
            //{
            //    return Search.SearchTweets("Happy Hour");
            //});

            var program = new Program();
            var javiPage = program.getJavistaPage();
            //var javitweets = program.getJaviTweetsByPage(javiPage);

            ////program.javistaInfo();
            ////List<ITweet> retweets = program.getRetweetedTweets(javiPage);
            //Hashtable retweets = program.getRetweetedTweets(javiPage);
            //var users = program.getRetweetersByTweetID(841939666022612994, retweets);

            //var numFollowers = program.getFollowersCount(javiPage);
            //var Followers = program.getFollowers(javiPage);

            var tweetsByTime = program.getJaviTweetsByTimeInterval(javiPage);
        }
    }
}