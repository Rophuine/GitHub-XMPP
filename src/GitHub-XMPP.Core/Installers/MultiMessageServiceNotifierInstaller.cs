using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.Messaging;

namespace GitHub_XMPP.Installers
{
    public class MessageNotifierInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<MultiMessageServiceNotifier>()
                       .BasedOn<IEventNotifier>()
                       .LifestyleSingleton()
                       .WithServiceBase());
        }
    }
}