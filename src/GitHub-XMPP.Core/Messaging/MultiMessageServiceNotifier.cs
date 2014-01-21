using Castle.Core.Internal;
using GitHub_XMPP.EventServices;

namespace GitHub_XMPP.Messaging
{
    public class MultiMessageServiceNotifier : IEventNotifier
    {
        public void SendText(string text)
        {
            IMessagingService[] messengers = IoC.Container.ResolveAll<IMessagingService>();
            messengers.ForEach(m =>
            {
                try
                {
                    m.SendText(text);
                }
                catch
                {
                    // TODO set up some logging framework!
                }
            });
            IMessagingServiceWithPresence[] presenceMessengers =
                IoC.Container.ResolveAll<IMessagingServiceWithPresence>();
            presenceMessengers.ForEach(m =>
            {
                try
                {
                    m.SendText(text);
                }
                catch
                {
                    // TODO set up some logging framework!
                }
            });
        }
    }
}