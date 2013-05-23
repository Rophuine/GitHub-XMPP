using GitHub_XMPP.GitHubDtos;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubIssueEventData
    {
        public string action { get; set; }
        public GitHubIssue issue { get; set; }
    }
}