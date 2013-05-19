using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.Notifiers;

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
            var c = container.Resolve<IEventNotifier>();
        }
    }
}