using GitHub_XMPP.GitHub.DTOs;

namespace GitHub_XMPP.GitHub.EventHandlers.DTOs
{
    public class GitHubMemberEventData
    {
        public GitHubUser member { get; set; }
        public string action { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}