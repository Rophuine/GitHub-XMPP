using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPullRequestReviewCommentEventData
    {
        public GitHubPullRequestComment comment { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}