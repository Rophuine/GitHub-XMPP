using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.GitHub;
using GitHub_XMPP.Services;

namespace GitHub_XMPP.Installers
{
    public class DomainEventsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<GitHubEventMapper>().BasedOn(typeof (IHandle<>))
                                      .WithServiceBase()
                                      .LifestyleSingleton());
            container.Register(Classes.FromAssemblyContaining<WindsorEventBroker>().BasedOn(typeof (IEventBroker))
                                      .WithServiceBase()
                                      .LifestyleSingleton());
        }
    }
}