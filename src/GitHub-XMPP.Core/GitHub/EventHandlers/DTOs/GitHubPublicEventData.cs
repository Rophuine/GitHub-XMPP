using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPublicEventData
    {
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}