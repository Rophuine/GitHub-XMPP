using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.XMPP.Bot;

namespace GitHub_XMPP.Installers
{
    public class XMPPBotInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<GitBot>()
                       .BasedOn(typeof (GitBot))
                       .LifestyleSingleton()
                       .WithServiceBase());
        }
    }
}
