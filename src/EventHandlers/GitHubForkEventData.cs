using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventServices
{
    public class GitHubForkEventData
    {
        public GitHubRepository forkee { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}