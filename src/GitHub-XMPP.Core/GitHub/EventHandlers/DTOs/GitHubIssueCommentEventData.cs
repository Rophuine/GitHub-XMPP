using GitHub_XMPP.GitHub.DTOs;

namespace GitHub_XMPP.GitHub.EventHandlers.DTOs
{
    public class GitHubIssueCommentEventData
    {
        public string action { get; set; }
        public GitHubIssue issue { get; set; }
        public GitHubIssueComment comment { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }
    }
}