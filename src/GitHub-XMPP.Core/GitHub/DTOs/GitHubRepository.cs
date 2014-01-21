using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.DTOs
{
    public class GitHubRepository
    {
        private string _fullName;
        public string name { get; set; }
        public string url { get; set; }
        public string pledgie { get; set; }
        public string description { get; set; }
        public string homepage { get; set; }
        public int watchers { get; set; }
        public int forks { get; set; }

        public int id { get; set; }

        public string full_name
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_fullName)) return _fullName;
                if (!string.IsNullOrWhiteSpace(url))
                {
                    Regex nameFinder = new Regex(".*github.com/(.*)");
                    if (nameFinder.IsMatch(url))
                        return nameFinder.Match(url).Groups[1].ToString();
                }
                return "";
            }
            set { _fullName = value; }
        }

        public GitHubUser owner { get; set; }
        public string html_url { get; set; }
        public bool fork { get; set; }
        public string forks_url { get; set; }
        public string keys_url { get; set; }
        public string collaborators_url { get; set; }
        public string teams_url { get; set; }
        public string hooks_url { get; set; }
        public string issue_events_url { get; set; }
        public string events_url { get; set; }
        public string assignees_url { get; set; }
        public string branches_url { get; set; }
        public string tags_url { get; set; }
        public string blobs_url { get; set; }
        public string git_tags_url { get; set; }
        public string git_refs_url { get; set; }
        public string trees_url { get; set; }
        public string statuses_url { get; set; }
        public string languages_url { get; set; }
        public string stargazers_url { get; set; }
        public string contributors_url { get; set; }
        public string subscribers_url { get; set; }
        public string subscription_url { get; set; }
        public string commits_url { get; set; }
        public string git_commits_url { get; set; }
        public string comments_url { get; set; }
        public string issue_comment_url { get; set; }
        public string contents_url { get; set; }
        public string compare_url { get; set; }
        public string merges_url { get; set; }
        public string archive_url { get; set; }
        public string downloads_url { get; set; }
        public string issues_url { get; set; }
        public string pulls_url { get; set; }
        public string milestones_url { get; set; }
        public string notifications_url { get; set; }
        public string labels_url { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string pushed_at { get; set; }
        public string git_url { get; set; }
        public string ssh_url { get; set; }
        public string clone_url { get; set; }
        public string svn_url { get; set; }
        public int size { get; set; }
        public int watchers_count { get; set; }
        public string language { get; set; }
        public bool has_issues { get; set; }
        public bool has_downloads { get; set; }
        public bool has_wiki { get; set; }
        public int forks_count { get; set; }
        public object mirror_url { get; set; }
        public int open_issues_count { get; set; }
        public int open_issues { get; set; }
        public string master_branch { get; set; }
        public string default_branch { get; set; }

        [JsonProperty(PropertyName = "private")]
        public bool privateRepo { get; set; }
    }
}