using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.EventServices;

namespace GitHub_XMPP.Installers
{
    public class DomainEventsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<GitHubEventMapper>().BasedOn(typeof (IHandle<>))
                                      .WithServiceBase()
                                      .LifestyleTransient());
        }
    }
}