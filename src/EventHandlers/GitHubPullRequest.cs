using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPullRequest
    {
        public string url { get; set; }
        public int id { get; set; }
        public string html_url { get; set; }
        public string diff_url { get; set; }
        public string patch_url { get; set; }
        public string issue_url { get; set; }
        public int number { get; set; }
        public string state { get; set; }
        public string title { get; set; }
        public GitHubUser user { get; set; }
        public string body { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object closed_at { get; set; }
        public object merged_at { get; set; }
        public string merge_commit_sha { get; set; }
        public object assignee { get; set; }
        public object milestone { get; set; }
        public string commits_url { get; set; }
        public string review_comments_url { get; set; }
        public string review_comment_url { get; set; }
        public string comments_url { get; set; }
        public GitHubCommit head { get; set; }
        public LinkSet _links { get; set; }
        public bool merged { get; set; }
        public object mergeable { get; set; }
        public string mergeable_state { get; set; }
        public object merged_by { get; set; }
        public int comments { get; set; }
        public int review_comments { get; set; }
        public int commits { get; set; }
        public int additions { get; set; }
        public int deletions { get; set; }
        public int changed_files { get; set; }

        [JsonProperty(PropertyName = "base")]
        public GitHubCommit setBase { get; set; }

        public class LinkSet
        {
            public Link self { get; set; }
            public Link html { get; set; }
            public Link issue { get; set; }
            public Link comments { get; set; }
            public Link review_comments { get; set; }
        }

        public class Link
        {
            public string href { get; set; }
        }
    }
}