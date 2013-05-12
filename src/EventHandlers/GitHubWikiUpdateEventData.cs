using System.Collections.Generic;
using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubWikiUpdateEventData
    {
        public List<WikiPageUpdateDetails> pages { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}