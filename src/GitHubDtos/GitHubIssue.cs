using System.Collections.Generic;

namespace GitHub_XMPP.GitHubDtos
{
    public class GitHubIssue
    {
        public string url { get; set; }
        public string labels_url { get; set; }
        public string comments_url { get; set; }
        public string events_url { get; set; }
        public string html_url { get; set; }
        public int id { get; set; }
        public int number { get; set; }
        public string state { get; set; }
        public string title { get; set; }
        public string body { get; set; }
        public GitHubUser user { get; set; }
        public List<Label> labels { get; set; }
        public GitHubUser assignee { get; set; }
        public Milestone milestone { get; set; }
        public int comments { get; set; }
        public GitHubPullRequest pull_request { get; set; }
        public string closed_at { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        public class Label
        {
            public string url { get; set; }
            public string name { get; set; }
            public string color { get; set; }
        }

        public class Milestone
        {
            public string url { get; set; }
            public int number { get; set; }
            public string state { get; set; }
            public string title { get; set; }
            public string description { get; set; }
            public GitHubUser creator { get; set; }
            public int open_issues { get; set; }
            public int closed_issues { get; set; }
            public string created_at { get; set; }
            public object due_on { get; set; }
        }
    }
}