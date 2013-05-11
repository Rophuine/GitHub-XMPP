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

        public void Handle(string jsonData)
        {
            var eventData = JsonConvert.DeserializeObject<GitHubPushEventData>(jsonData);
            var sb = new StringBuilder();
            sb.AppendLine(string.Format("{0} has pushed new commits to {1}/{2}:", eventData.pusher.name,
                                        eventData.repository.owner.login, eventData.repository.name));
            foreach (GitHubPushEventData.Commit commit in eventData.commits)
            {
                sb.AppendLine(commit.GetMessageWithoutDoubledLineBreak());
                sb.AppendLine(string.Format("(Committed by {0} at {1} - {2})", commit.author.username, commit.timestamp,
                                            commit.url));
            }
            sb.Append(string.Format("View the entire change-set at {0}", eventData.compareUrl));
            _eventNotifier.SendText(sb.ToString());
        }
    }
}