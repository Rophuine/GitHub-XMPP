namespace GitHub_XMPP.EventServices
{
    public class GitHubIssueCommentEventData
    {
        public int id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string body { get; set; }
        public GitHubUser user { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}