using System.Text;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPullRequestEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubPullRequestEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void Handle(string jsonData)
        {
            var eventData = JsonConvert.DeserializeObject<GitHubPullRequestEventData>(jsonData);

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} {1} pull request {2} on {3} ({4})", eventData.sender.login,
                                    eventData.action, eventData.pull_request.title, eventData.repository.name,
                                    eventData.pull_request.html_url));
            if (!string.IsNullOrWhiteSpace(eventData.pull_request.body))
            {
                sb.AppendLine();
                sb.Append(eventData.pull_request.body);
            }

            _eventNotifier.SendText(sb.ToString());
        }
    }
}