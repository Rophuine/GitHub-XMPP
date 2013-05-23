using System;

namespace GitHub_XMPP.Services
{
    public interface IServiceLocator : IServiceProvider
    {
        TServiceType[] ResolveAll<TServiceType>();
        TServiceType Resolve<TServiceType>();
        object Resolve(Type serviceType);
    }
}