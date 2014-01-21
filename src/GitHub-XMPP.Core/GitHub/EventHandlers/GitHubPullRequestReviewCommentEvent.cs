using System.Text;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.GitHub.EventHandlers.DTOs;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.EventHandlers
{
    public class GitHubPullRequestReviewCommentEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubPullRequestReviewCommentEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubPullRequestReviewCommentEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubPullRequestReviewCommentEventData>(jsonData);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} has commented on a pull request on {1}/{2} ({3})", EventData.sender.login,
                EventData.repository.owner.login, EventData.repository.name,
                EventData.comment.html_url));
            sb.Append(EventData.comment.body);

            _eventNotifier.SendText(sb.ToString());
        }
    }
}