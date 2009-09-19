using NSBManager.Instrumentation.Core;
using NServiceBus;
using NServiceBus.Host;
using NServiceBus.ObjectBuilder;

namespace NSBManager.Instrumentation.UnitTests.GenericHost
{
    public class Configuration : IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Instance.Configurer.ConfigureComponent<EndpointMonitor>(ComponentCallModelEnum.Singleton);
        }
    }
}