using System.Text;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventServices
{
    public class GitHubPushEvent
    {
        public GitHubPushEventData EventData { get; set; }
        private readonly IEventNotifier _eventNotifier;

        public GitHubPushEvent(IEventNotifier eventNotifier, string jsonData)
        {
            _eventNotifier = eventNotifier;
            EventData = JsonConvert.DeserializeObject<GitHubPushEventData>(jsonData);
        }

        public void Handle()
        {
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} has pushed new commits to {1}/{2}:", EventData.pusher.name, EventData.repository.owner.name, EventData.repository.name));
            foreach (var commit in EventData.commits)
            {
                sb.AppendLine(commit.message);
                sb.AppendLine(string.Format("(Committed by {0} at {1} - {2})", commit.author.username, commit.timestamp, commit.url));
            }
            sb.AppendLine(string.Format("View the entire change-set at {0}", EventData.compareUrl));
            _eventNotifier.SendText(sb.ToString());
        }
    }
}