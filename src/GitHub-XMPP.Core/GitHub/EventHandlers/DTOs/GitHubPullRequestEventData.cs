using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPullRequestEventData
    {
        public string action { get; set; }
        public int number { get; set; }
        public GitHubPullRequest pull_request { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}