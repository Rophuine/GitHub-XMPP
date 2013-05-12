using System.Text;
using GitHub_XMPP.GitHubDtos;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;

namespace GitHub_XMPP.EventHandlers
{
    public class GitHubWikiUpdateEvent : IGitHubEventHandler
    {
        private readonly IEventNotifier _eventNotifier;

        public GitHubWikiUpdateEvent(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
        }

        public void Handle(string jsonData)
        {
            var eventData = JsonConvert.DeserializeObject<GitHubWikiUpdateEventData>(jsonData);

            var sb = new StringBuilder();
            sb.Append(string.Format("{0} has made the following changes to the wiki for {1}{2}:",
                                    eventData.sender.login, eventData.repository.owner.login,
                                    eventData.repository.name));
            foreach (WikiPageUpdateDetails pageUpdate in eventData.pages)
            {
                sb.AppendLine();
                sb.Append(string.Format("{0} {1} {2} ({3})", pageUpdate.action, pageUpdate.page_name,
                                        pageUpdate.summary ?? "(no summary available)", pageUpdate.html_url));
            }

            _eventNotifier.SendText(sb.ToString());
        }
    }
}