namespace GitHub_XMPP.GitHubDtos
{
    public class GitHubCommitComment
    {
        public string html_url { get; set; }
        public string url { get; set; }
        public int id { get; set; }
        public string body { get; set; }
        public string path { get; set; }
        public string position { get; set; }
        public string line { get; set; }
        public string commit_id { get; set; }
        public GitHubUser user { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }
}