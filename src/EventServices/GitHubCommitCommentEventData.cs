﻿namespace GitHub_XMPP.EventServices
{
    public class GitHubCommitCommentEventData
    {
        public GitHubCommitComment comment { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}