using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.GitHub.EventHandlers;

namespace GitHub_XMPP.Installers
{
    public class GitHubEventInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<GitHubPushEvent>()
                                      .BasedOn(typeof (IGitHubEventHandler))
                                      .WithServiceSelf()
                                      .LifestyleTransient());
        }
    }
}