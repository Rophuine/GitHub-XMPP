using System.Text.RegularExpressions;
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
}