namespace GitHub_XMPP.Services
{
    public class WindsorEventBroker : IEventBroker
    {
        private readonly IServiceLocator _locator;

        public WindsorEventBroker(IServiceLocator locator)
        {
            _locator = locator;
        }

        public void Raise<TEventType>(TEventType eventObj) where TEventType : IDomainEvent
        {
            IHandle<TEventType>[] handlers = _locator.ResolveAll<IHandle<TEventType>>();
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