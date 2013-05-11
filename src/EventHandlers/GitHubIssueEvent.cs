using System.Text;
using GitHub_XMPP.EventHandlers;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventServices
{
    public class GitHubIssueEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubIssueEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void Handle(string jsonData)
        {
            var eventData = JsonConvert.DeserializeObject<GitHubIssueEventData>(jsonData);
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} just {1} issue {2}", eventData.issue.user.login, eventData.action,
                                        eventData.issue.number));
            string assignee = (eventData.issue.assignee != null) ? eventData.issue.assignee.login : "unassigned";
            sb.Append(string.Format("{0} - {1} ({2})", eventData.issue.title, assignee,
                                        eventData.issue.html_url));
            _eventNotifier.SendText(sb.ToString());
        }
    }

    public class GitHubIssueEventData
    {
        public string action { get; set; }
        public GitHubIssue issue { get; set; }
    }
}