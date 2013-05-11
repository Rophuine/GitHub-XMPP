using System.Text;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPullRequestReviewCommentEvent : IGitHubEventHandler
    {
        private IEventNotifier _eventNotifier;

        public GitHubPullRequestReviewCommentEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }


        public void Handle(string jsonData)
        {
            var eventData = JsonConvert.DeserializeObject<GitHubPullRequestReviewCommentEventData>(jsonData);

            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} has commented on a pull request on {1}/{2} ({3})", eventData.sender.login,
                                        eventData.repository.owner.login, eventData.repository.name,
                                        eventData.comment.html_url));
            sb.Append(eventData.comment.body);

            _eventNotifier.SendText(sb.ToString());
        }
    }

    public class GitHubPullRequestReviewCommentEventData
    {
        public GitHubPullRequestComment comment { get; set; }
        public GitHubRepository repository { get; set; }
        public GitHubUser sender { get; set; }        
    }
}