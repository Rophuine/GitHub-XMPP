using System;

namespace GitHub_XMPP.EventServices
{
    public static class DomainEvents
    {
        private static IEventBroker _eventBroker;

        public static void SetEventBrokerStrategy(IEventBroker eventBroker)
        {
            _eventBroker = eventBroker;
        }

        public static void Raise<TEventType>(TEventType eventObj) where TEventType : IDomainEvent
        {
            IEventBroker broker = _eventBroker;
            if (broker == null)
                throw new InvalidOperationException("You must provide an event broker before raising an event.");
            broker.Raise(eventObj);
        }
    }

    public interface IEventBroker
    {
        void Raise<TEventType>(TEventType eventObj) where TEventType : IDomainEvent;
    }

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