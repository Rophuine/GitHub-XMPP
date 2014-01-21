namespace GitHub_XMPP.EventServices
{
    public class WindsorEventBroker : IEventBroker
    {
        public void Raise<TEventType>(TEventType eventObj) where TEventType : IDomainEvent
        {
            // FIXME we shouldn't know about our container
            IHandle<TEventType>[] handlers = IoC.Container.ResolveAll<IHandle<TEventType>>();
            foreach (var handler in handlers)
            {
                try
                {
                    handler.Handle(eventObj);
                }
                finally
                {
                    IoC.Container.Release(handler);
                }
            }
        }
    }
}