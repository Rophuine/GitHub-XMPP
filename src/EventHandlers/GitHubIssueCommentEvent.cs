using System.Text;
using GitHub_XMPP.EventHandlers;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventServices
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
            sb.AppendLine(string.Format("{0} commented on issue {1} ({2})", eventData.user.login, eventData.id,
                                        eventData.html_url));
            sb.Append(eventData.body);

            _eventNotifier.SendText(sb.ToString());
        }
    }
}