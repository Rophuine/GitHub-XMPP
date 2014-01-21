using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.Messaging;

namespace GitHub_XMPP.Installers
{
    public class MessagingServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<MessagingServicesInstaller>()
                .BasedOn<IMessagingService>()
                .LifestyleTransient()
                .WithServiceBase());
            container.Register(Classes.FromAssemblyContaining<MessagingServicesInstaller>()
                .BasedOn<IMessagingServiceWithPresence>()
                .LifestyleSingleton()
                .WithServiceBase());
            IMessagingServiceWithPresence[] c = container.ResolveAll<IMessagingServiceWithPresence>();
                // Grab the singletons immediately to trigger presence
        }
    }
}