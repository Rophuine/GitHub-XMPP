namespace GitHub_XMPP.GitHub.DTOs
{
    public class GitHubCommit
    {
        public string label { get; set; }
        public string @ref { get; set; }
        public string sha { get; set; }
        public GitHubUser user { get; set; }
        public GitHubRepository repo { get; set; }
    }
}