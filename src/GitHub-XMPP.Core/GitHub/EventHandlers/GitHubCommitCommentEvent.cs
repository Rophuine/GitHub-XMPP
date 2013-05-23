using System.Text;
using GitHub_XMPP.GitHub.EventHandlers.DTOs;
using GitHub_XMPP.Services;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.EventHandlers
{
    public class GitHubCommitCommentEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubCommitCommentEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubCommitCommentEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubCommitCommentEventData>(jsonData);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} commented on commit {1} ({2})", EventData.sender.login,
                                        EventData.comment.commit_id, EventData.comment.html_url));
            sb.Append(EventData.comment.body);
            _eventNotifier.SendText(sb.ToString());
        }
    }
}