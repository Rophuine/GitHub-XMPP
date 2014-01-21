using Castle.Core.Internal;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.HipChat;
using GitHub_XMPP.XMPP;

namespace GitHub_XMPP.Messaging
{
    public class MultiMessageServiceNotifier : IEventNotifier
    {
        public void SendText(string text)
        {
            var messengers = IoC.Container.ResolveAll<IMessagingService>();
            messengers.ForEach(m => m.SendText(text));
            var presenceMessengers = IoC.Container.ResolveAll<IMessagingServiceWithPresence>();
            presenceMessengers.ForEach(m => m.SendText(text));
        }
    }
}