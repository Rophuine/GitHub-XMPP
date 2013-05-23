namespace GitHub_XMPP.EventHandlers
{
    internal interface IGitHubEventHandler
    {
        void Handle(string jsonData);
    }
}