namespace GitHub_XMPP.Services
{
    public interface IHandle<in TEventType> where TEventType : IDomainEvent
    {
        void Handle(TEventType eventObject);
    }
}