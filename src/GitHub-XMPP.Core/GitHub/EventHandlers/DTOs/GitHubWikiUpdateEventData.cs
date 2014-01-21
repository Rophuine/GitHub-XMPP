using System.Collections.Generic;
using GitHub_XMPP.GitHub.DTOs;

namespace GitHub_XMPP.GitHub.EventHandlers.DTOs
{
    public class GitHubWikiUpdateEventData
    {
        public List<WikiPageUpdateDetails> pages { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}