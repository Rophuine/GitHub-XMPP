using System.Text;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.GitHub.EventHandlers.DTOs;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.EventHandlers
{
    public class GitHubIssueCommentEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubIssueCommentEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubIssueCommentEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubIssueCommentEventData>(jsonData);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} {1} a comment on issue {2} ({3})", EventData.sender.login, EventData.action,
                                        EventData.issue.title, EventData.issue.html_url));
            sb.Append(EventData.comment.body);

            _eventNotifier.SendText(sb.ToString());
        }
    }
}