namespace GitHub_XMPP.GitHubDtos
{
    public class GitHubPullRequestComment
    {
        public string url { get; set; }
        public int id { get; set; }
        public string body { get; set; }
        public string diff_hunk { get; set; }
        public string path { get; set; }
        public int position { get; set; }
        public int original_position { get; set; }
        public string commit_id { get; set; }
        public string original_commit_id { get; set; }
        public GitHubUser user { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string html_url { get; set; }
        public string pull_request_url { get; set; }
        public GitHubPullRequest.LinkSet _links { get; set; }
    }
}