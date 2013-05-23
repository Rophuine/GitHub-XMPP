using System.Text;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubMemberEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubMemberEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubMemberEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubMemberEventData>(jsonData);

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} just {1} {2} on {3} ({4})", EventData.sender.login, EventData.action,
                                    EventData.member.login, EventData.repository.full_name,
                                    EventData.repository.html_url));
            if (EventData.action == "added")
            {
                sb.AppendLine();
                sb.Append(string.Format("Welcome aboard, {0}!", EventData.member.login));
            }

            // TODO: Would it be cool to invite them to the room if they're not already in it? We could probably get their email

            _eventNotifier.SendText(sb.ToString());
        }
    }
}