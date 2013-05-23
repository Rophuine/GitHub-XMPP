using GitHub_XMPP.GitHub.DTOs;

namespace GitHub_XMPP.GitHub.EventHandlers.DTOs
{
    public class GitHubPublicEventData
    {
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}