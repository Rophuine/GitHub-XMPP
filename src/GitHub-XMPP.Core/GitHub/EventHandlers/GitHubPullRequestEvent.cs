using System.Text;
using GitHub_XMPP.GitHub.EventHandlers.DTOs;
using GitHub_XMPP.Services;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.EventHandlers
{
    public class GitHubPullRequestEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubPullRequestEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubPullRequestEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubPullRequestEventData>(jsonData);

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} {1} pull request {2} on {3} ({4})", EventData.sender.login,
                                    EventData.action, EventData.pull_request.title, EventData.repository.name,
                                    EventData.pull_request.html_url));
            if (!string.IsNullOrWhiteSpace(EventData.pull_request.body))
            {
                sb.AppendLine();
                sb.Append(EventData.pull_request.body);
            }

            _eventNotifier.SendText(sb.ToString());
        }
    }
}