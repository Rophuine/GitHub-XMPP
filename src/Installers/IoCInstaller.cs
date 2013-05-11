using GitHub_XMPP.Notifiers;
using Nancy.TinyIoc;

namespace GitHub_XMPP.Installers
{
    public static class IoCInstaller
    {
        public static void Install()
        {
            TinyIoCContainer container = TinyIoCContainer.Current;
            container.AutoRegister();
            container.Resolve<IEventNotifier>(); // Create our singleton right away
        }
    }
}