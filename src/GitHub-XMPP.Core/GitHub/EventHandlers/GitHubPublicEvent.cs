using System.Text;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.GitHub.EventHandlers.DTOs;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.EventHandlers
{
    public class GitHubPublicEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubPublicEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubPublicEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubPublicEventData>(jsonData);

            var sb = new StringBuilder();
            sb.Append(string.Format("Woah! {0} just made {1} public! Nice.", EventData.sender.login,
                                    EventData.repository.full_name));

            _eventNotifier.SendText(sb.ToString());
        }
    }
}