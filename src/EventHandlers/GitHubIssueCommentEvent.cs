using System.Text;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubIssueCommentEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubIssueCommentEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }


        public void Handle(string jsonData)
        {
            var eventData = JsonConvert.DeserializeObject<GitHubIssueCommentEventData>(jsonData);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} {1} a comment on issue {2} ({3})", eventData.sender.login, eventData.action,
                                        eventData.issue.title, eventData.issue.html_url));
            sb.Append(eventData.comment.body);

            _eventNotifier.SendText(sb.ToString());
        }
    }
}