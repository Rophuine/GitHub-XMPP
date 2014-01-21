using GitHub_XMPP.EventServices;
using GitHub_XMPP.XMPP.Events;

namespace GitHub_XMPP.XMPP.Bot
{
    public class GitBotNotifier : IHandle<GroupChatMessageArrived>
    {
        public void Handle(GroupChatMessageArrived eventObject)
        {
            // FIXME this shouldn't have to know about the container
            GitBot[] bots = IoC.Container.ResolveAll<GitBot>();
            foreach (GitBot bot in bots)
            {
                try
                {
                    bot.Handle(eventObject);
                }
                finally
                {
                    IoC.Container.Release(bot);
                }
            }
        }
    }
}