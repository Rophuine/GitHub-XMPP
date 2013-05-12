using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubCommitCommentEventData
    {
        public GitHubCommitComment comment { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}