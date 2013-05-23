using System.Text;
using GitHub_XMPP.GitHub.DTOs;
using GitHub_XMPP.GitHub.EventHandlers.DTOs;
using GitHub_XMPP.Services;
using Newtonsoft.Json;

namespace GitHub_XMPP.GitHub.EventHandlers
{
    public class GitHubWikiUpdateEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubWikiUpdateEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public GitHubWikiUpdateEventData EventData { get; set; }

        public void Handle(string jsonData)
        {
            EventData = JsonConvert.DeserializeObject<GitHubWikiUpdateEventData>(jsonData);

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} has made the following changes to the wiki for {1}:",
                                    EventData.sender.login, EventData.repository.full_name));
            foreach (WikiPageUpdateDetails pageUpdate in EventData.pages)
            {
                sb.AppendLine();
                sb.Append(string.Format("{0} {1} {2} ({3})", pageUpdate.action, pageUpdate.page_name,
                                        pageUpdate.summary ?? "(no summary available)", pageUpdate.html_url));
            }

            _eventNotifier.SendText(sb.ToString());
        }
    }
}