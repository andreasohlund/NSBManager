using NSBManager.Instrumentation.Core;
using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Inspectors.Host;
using NSBManager.Instrumentation.Core.Inspectors.Transport;
using NServiceBus.ObjectBuilder;

namespace NServiceBus
{
    public static class ConfigureExtensions
    {
        public static Configure EnableInstrumentation(this Configure config)
        {
            return EnableInstrumentation(config,new ExeHostInspector());
        }

        public static Configure EnableInstrumentation(this Configure config, IHostInspector hostInspector)
        {
            config.Configurer.ConfigureComponent<EndpointMonitor>(ComponentCallModelEnum.Singlecall);
            config.Configurer.ConfigureComponent<MsmqTransportInspector>(ComponentCallModelEnum.Singlecall);

            config.Configurer.RegisterSingleton(typeof (IHostInspector), hostInspector);

            return config;
        }
    }
}