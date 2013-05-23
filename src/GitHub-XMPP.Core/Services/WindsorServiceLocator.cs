using System;

namespace GitHub_XMPP.Services
{
    public class WindsorServiceLocator : IServiceLocator
    {
        public object GetService(Type serviceType)
        {
            return IoC.Container.Resolve(serviceType);
        }

        public TServiceType[] ResolveAll<TServiceType>()
        {
            return IoC.Container.ResolveAll<TServiceType>();
        }

        public TServiceType Resolve<TServiceType>()
        {
            return IoC.Container.Resolve<TServiceType>();
        }

        public object Resolve(Type serviceType)
        {
            return IoC.Container.Resolve(serviceType);
        }
    }
}