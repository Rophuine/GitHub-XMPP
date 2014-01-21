namespace GitHub_XMPP.EventServices
{
    public interface IEventBroker
    {
        void Raise<TEventType>(TEventType eventObj) where TEventType : IDomainEvent;
    }
}