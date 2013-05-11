using System.Text;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubCommitCommentEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubCommitCommentEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void Handle(string jsonData)
        {
            var eventData = JsonConvert.DeserializeObject<GitHubCommitCommentEventData>(jsonData);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} commented on commit {1} ({2})", eventData.sender.login,
                                        eventData.comment.commit_id, eventData.comment.html_url));
            sb.Append(eventData.comment.body);
            _eventNotifier.SendText(sb.ToString());
        }
    }
}