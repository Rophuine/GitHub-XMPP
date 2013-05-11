using GitHub_XMPP.EventServices;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubIssueComment
    {
        public string url { get; set; }
        public string html_url { get; set; }
        public string issue_url { get; set; }
        public int id { get; set; }
        public GitHubUser user { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string body { get; set; }
    }
}