using System;
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
        static void Main(string[] args)
        {
            Auth.SetUserCredentials("IVVasZNY6Fkdwi32BOVKuuXvG", "6KSfzns39G0Wjsw0LS8P3eayQsIZuFHVEUS3nYeJlIizK1vrhi", "838041203895586816-5GkpOpuLUJq17nYGcHEAZrf6IOI2Dvx", "UFnrldn285PnO8LOPFF5Ms8R5VCd13SfkkWLG4GEiGT8g");
            var user = User.GetAuthenticatedUser();
            var tweets = Search.SearchTweets("Happy Hour");
            var timeline = Timeline.GetHomeTimeline();

            var searchparam = new Tweetinvi.Parameters.SearchTweetsParameters("Obama")
            {
                MaximumNumberOfResults = 30,
                //SearchType = SearchResultType.Popular,
                //Until = new DateTime(2015, 10, 10),
                //Since = new DateTime(2010, 10, 10)
            };
            var tweetss = Search.SearchTweets(searchparam);

            var users = Search.SearchUsers("Javista");

            var javistaFollowers = users.ToList()[2].FollowersCount;
            var javistaF = users.ToList()[2].GetFollowers();


            //autre methode pr credentials
            //var creds = new TwitterCredentials("eexE3I6qTZ6KldSOGqDBa3jkw", "NXLLQeJ4wINGfJ2rouDCsZLpnqCfd9cfVqDJUtjpk7jwfiwYpe", "416698831-jvYXtAmRrjijesP0sBf6E1UkX84dFlaUjPlzSDuA", "gW67pNo3z0WE0tSordiyWK8cuWKApM64HganFnN0g3vcR");
            //var tweet = Auth.ExecuteOperationWithCredentials(creds, () =>
            //{
            //    return Search.SearchTweets("Happy Hour");
            //});
        }
    }
}
