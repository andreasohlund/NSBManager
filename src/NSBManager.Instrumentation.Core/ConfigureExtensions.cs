using NSBManager.Instrumentation.Core;
using NSBManager.Instrumentation.Core.Inspectors;
using NServiceBus.ObjectBuilder;

namespace NServiceBus
{
    public static class ConfigureExtensions
    {
        public static Configure EnableInstrumentation(this Configure config)
        {
            config.Configurer.ConfigureComponent<EndpointMonitor>(ComponentCallModelEnum.Singlecall);
            config.Configurer.ConfigureComponent<MsmqTransportInspector>(ComponentCallModelEnum.Singlecall);
            return config;
        }
    }
}