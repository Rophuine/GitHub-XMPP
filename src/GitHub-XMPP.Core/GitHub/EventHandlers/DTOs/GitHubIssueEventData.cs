using GitHub_XMPP.GitHub.DTOs;

namespace GitHub_XMPP.GitHub.EventHandlers.DTOs
{
    public class GitHubIssueEventData
    {
        public string action { get; set; }
        public GitHubIssue issue { get; set; }
    }
}