using System.Text.RegularExpressions;
using GitHub_XMPP.Services;
using GitHub_XMPP.XMPP.Events;

namespace GitHub_XMPP.XMPP.Bot
{
    public abstract class GitBot
    {
        public Regex MessageFilter { get; set; }

        public abstract void ReceiveGroupMessage(GroupChatMessageArrived message, MatchCollection matches);

        public virtual bool TestMessageFilter(GroupChatMessageArrived eventObj)
        {
            return (MessageFilter.IsMatch(eventObj.Message.Body));
        }
        
        public void Handle(GroupChatMessageArrived eventObject)
        {
            if (TestMessageFilter(eventObject))
            {
                ReceiveGroupMessage(eventObject, MessageFilter.Matches(eventObject.Message.Body));
            }
        }
    }

    public class GitBotNotifier : IHandle<GroupChatMessageArrived>
    {
        public void Handle(GroupChatMessageArrived eventObject)
        {
            // FIXME this shouldn't have to know about the container
            var bots = IoC.Container.ResolveAll<GitBot>();
            foreach (var bot in bots)
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