using System.Text;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubForkEvent : IGitHubEventHandler
    {
        private IEventNotifier _eventNotifier;

        public GitHubForkEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubForkEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubForkEventData>(jsonData);

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} just forked {1}/{2} ({3})", EventData.sender.login,
                                    EventData.repository.owner.login, EventData.repository.name,
                                    EventData.forkee.html_url));

            _eventNotifier.SendText(sb.ToString());
        }
    }
}