using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.GitHub.EventHandlers;
using GitHub_XMPP.NodeEmu;
using GitHub_XMPP.Services;

namespace GitHub_XMPP.Installers
{
    public class ScriptEngineInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<GitHubPushEvent>()
                                      .BasedOn(typeof (IScriptEngine))
                                      .WithServiceBase()
                                      .LifestyleTransient());
        }
    }
}
