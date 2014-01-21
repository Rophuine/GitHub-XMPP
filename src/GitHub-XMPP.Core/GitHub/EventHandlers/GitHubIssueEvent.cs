using System.Text;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.GitHub.EventHandlers.DTOs;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.EventHandlers
{
    public class GitHubIssueEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubIssueEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubIssueEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubIssueEventData>(jsonData);
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} just {1} issue {2}", EventData.issue.user.login, EventData.action,
                EventData.issue.number));
            string assignee = (EventData.issue.assignee != null) ? EventData.issue.assignee.login : "unassigned";
            sb.Append(string.Format("{0} - {1} ({2})", EventData.issue.title, assignee,
                EventData.issue.html_url));
            _eventNotifier.SendText(sb.ToString());
        }
    }
}