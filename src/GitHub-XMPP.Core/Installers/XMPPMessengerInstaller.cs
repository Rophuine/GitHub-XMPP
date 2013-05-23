using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.Services;
using GitHub_XMPP.XMPP;

namespace GitHub_XMPP.Installers
{
    public class XMPPMessengerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<XMPPClient>()
                       .BasedOn<IEventNotifier>()
                       .LifestyleSingleton()
                       .WithServiceBase());
            var c = container.Resolve<IEventNotifier>();    // Grab the singleton immediate so it is instantiated
        }
    }
}