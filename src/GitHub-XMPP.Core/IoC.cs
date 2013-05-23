using Castle.Windsor;
using Castle.Windsor.Installer;
using GitHub_XMPP.GitHub;
using GitHub_XMPP.Installers;
using GitHub_XMPP.Services;

namespace GitHub_XMPP
{
    public static class IoC
    {
        public static WindsorContainer Container;

        public static void Install()
        {
            Container = new WindsorContainer();
            Container.Install(FromAssembly.Containing<GitHubHookInstaller>());
            DomainEvents.SetEventBrokerStrategy(Container.Resolve<IEventBroker>());
        }
    }
}