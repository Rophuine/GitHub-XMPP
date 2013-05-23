using System;

namespace GitHub_XMPP.Services
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
}