using System.Linq;
using System.Text;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubPushEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubPushEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubPushEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubPushEventData>(jsonData);
            var sb = new StringBuilder();
            var branch = EventData.GetBranchName();
            sb.AppendLine(string.Format("{0} has pushed new commits to {1} on {2}:", EventData.pusher.name,
                                        EventData.repository.full_name, branch));
            var firstCommit = EventData.commits.First();
            sb.Append(firstCommit.GetMessageWithoutDoubledLineBreak());
            if (EventData.commits.Length > 1)
                sb.AppendLine(string.Format(" (... and {0} additional commits ...)", EventData.commits.Length - 1));
            else sb.AppendLine();

            var branchUrl = EventData.GetBranchUrl();
            sb.Append(string.Format("View the latest commits to {0} at {1}", branch, branchUrl));

            _eventNotifier.SendText(sb.ToString());
        }

    }
}