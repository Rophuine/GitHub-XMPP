using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using GitHub_XMPP.NodeEmu.API;

namespace GitHub_XMPP.NodeEmu.Installers
{
    public class NodeAPIComponentInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining<NodeAPIComponentInstaller>()
                          .BasedOn(typeof(INodeAPIComponent))
                          .WithServiceSelf()
                          .LifestyleTransient());
        }
    }
}
