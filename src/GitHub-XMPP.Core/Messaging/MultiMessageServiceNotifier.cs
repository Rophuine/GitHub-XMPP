using Castle.Core.Internal;
using GitHub_XMPP.EventServices;

namespace GitHub_XMPP.Messaging
{
    public class MultiMessageServiceNotifier : IEventNotifier
    {
        public void SendText(string text)
        {
            IMessagingService[] messengers = IoC.Container.ResolveAll<IMessagingService>();
            messengers.ForEach(m => m.SendText(text));
            IMessagingServiceWithPresence[] presenceMessengers =
                IoC.Container.ResolveAll<IMessagingServiceWithPresence>();
            presenceMessengers.ForEach(m => m.SendText(text));
        }
    }
}