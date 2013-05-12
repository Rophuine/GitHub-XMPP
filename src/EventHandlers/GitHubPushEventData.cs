﻿using System;
using GitHub_XMPP.GitHubDtos;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPushEventData
    {
        public string before { get; set; }
        public string after { get; set; }
        public Author pusher { get; set; }

        [JsonProperty(PropertyName = "compare")]
        public string compareUrl { get; set; }

        [JsonProperty(PropertyName = "ref")]
        public string reference { get; set; }

        public CommitDetails[] commits { get; set; }
        public GitHubRepository repository { get; set; }

        public class CommitDetails
        {
            public string id { get; set; }
            public string message { get; set; }
            public DateTime timestamp { get; set; }
            public string url { get; set; }
            public string[] added { get; set; }
            public string[] removed { get; set; }
            public string[] modified { get; set; }
            public Author author { get; set; }
            public Author committer { get; set; }

            public string GetMessageWithoutDoubledLineBreak()
            {
                return message.Replace("\n\n", "\n");
            }
        }

        public class Author
        {
            public string name { get; set; }
            public string email { get; set; }
            public string username { get; set; }
        }
    }
}