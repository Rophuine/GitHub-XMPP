namespace GitHub_XMPP.GitHub.EventHandlers
{
    internal interface IGitHubEventHandler
    {
        void Handle(string jsonData);
    }
}