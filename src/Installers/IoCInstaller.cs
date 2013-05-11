using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GitHub_XMPP.Notifiers;
using Nancy.TinyIoc;

namespace GitHub_XMPP.Installers
{
    public static class IoCInstaller
    {
        public static void Install()
        {
            var container = TinyIoCContainer.Current;
            container.AutoRegister();
            container.Resolve<IEventNotifier>();    // Create our singleton right away
        }
    }
}
