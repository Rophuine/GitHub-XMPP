namespace GitHub_XMPP.Services
{
    public interface IEventBroker
    {
        void Raise<TEventType>(TEventType eventObj) where TEventType : IDomainEvent;
    }
}