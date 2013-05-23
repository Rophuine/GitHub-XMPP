namespace GitHub_XMPP.EventServices
{
    public interface IHandle<in TEventType> where TEventType : IDomainEvent
    {
        void Handle(TEventType eventObject);
    }
}