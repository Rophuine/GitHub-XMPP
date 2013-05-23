using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubMemberEventData
    {
        public GitHubUser member { get; set; }
        public string action { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}