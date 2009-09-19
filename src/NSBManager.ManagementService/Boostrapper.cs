using System;
using NServiceBus;
using NServiceBus.Host;
using NServiceBus.ObjectBuilder;

namespace NSBManager.ManagementService
{
    public class Boostrapper : IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Instance.Configurer.ConfigureComponent<ServiceBus>(ComponentCallModelEnum.Singleton);
        }
    }
}