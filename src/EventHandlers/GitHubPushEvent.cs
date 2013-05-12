using System.Text;
using GitHub_XMPP.EventServices;
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
            sb.AppendLine(string.Format("{0} has pushed new commits to {1}/{2}:", EventData.pusher.name,
                                        EventData.repository.owner.login, EventData.repository.name));
            var count = 0;
            foreach (GitHubPushEventData.CommitDetails commit in EventData.commits)
            {
                if (count < 2)
                {
                    count++;
                    sb.AppendLine(commit.GetMessageWithoutDoubledLineBreak());
                    sb.AppendLine(string.Format("(Committed by {0} at {1} - {2})", commit.author.username,
                                                commit.timestamp,
                                                commit.url));
                }
                else
                {
                    sb.AppendLine(string.Format("(... {0} additional commits ...)", EventData.commits.Length - count));
                    break;
                }
            }
            sb.Append(string.Format("View the entire change-set at {0}", EventData.compareUrl));
            _eventNotifier.SendText(sb.ToString());
        }
    }
}