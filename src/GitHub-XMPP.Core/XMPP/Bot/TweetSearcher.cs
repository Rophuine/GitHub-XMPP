using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.XMPP.Events;
using Newtonsoft.Json;

namespace GitHub_XMPP.XMPP.Bot
{
    public class TweetSearcher : GitBot
    {
        private readonly IEventNotifier _eventNotifier;

        public TweetSearcher(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
            MessageFilter = new Regex("(.*) tweet");
        }

        public override void ReceiveGroupMessage(GroupChatMessageArrived message, MatchCollection matches)
        {
            var search = matches[0].Groups[1];
            var client = new WebClient();
            var response = client.DownloadString(string.Format("http://search.twitter.com/search.json?q={0}", search));
            var queryResult = JsonConvert.DeserializeObject<TwitterQueryResult>(response);
            if (queryResult.results != null && queryResult.results.Count > 0)
            {
                _eventNotifier.SendText(string.Format("{0} tweeted about {1}:\n{2} ({3})",
                    queryResult.results[0].from_user,
                    search,
                    queryResult.results[0].text,
                    queryResult.results[0].GetTweetUrl()));
            }
        }

        private class Metadata
        {
            public string result_type { get; set; }
        }

        private class Result
        {
            public string created_at { get; set; }
            public string from_user { get; set; }
            public int from_user_id { get; set; }
            public string from_user_id_str { get; set; }
            public string from_user_name { get; set; }
            public object geo { get; set; }
            public object id { get; set; }
            public string id_str { get; set; }
            public string iso_language_code { get; set; }
            public Metadata metadata { get; set; }
            public string profile_image_url { get; set; }
            public string profile_image_url_https { get; set; }
            public string source { get; set; }
            public string text { get; set; }
            public string to_user { get; set; }
            public int to_user_id { get; set; }
            public string to_user_id_str { get; set; }
            public string to_user_name { get; set; }
            public object in_reply_to_status_id { get; set; }
            public string in_reply_to_status_id_str { get; set; }
            public string GetTweetUrl()
            {
                return string.Format("http://twitter.com/{0}/status/{1}", from_user, id);
            }
        }

        private class TwitterQueryResult
        {
            public double completed_in { get; set; }
            public long max_id { get; set; }
            public string max_id_str { get; set; }
            public string next_page { get; set; }
            public int page { get; set; }
            public string query { get; set; }
            public string refresh_url { get; set; }
            public List<Result> results { get; set; }
            public int results_per_page { get; set; }
            public int since_id { get; set; }
            public string since_id_str { get; set; }
        }
    }
}
