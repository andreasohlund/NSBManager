using NSBManager.Instrumentation.Core;
using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Inspectors.Host;
using NSBManager.Instrumentation.Core.Inspectors.Transport;
using NServiceBus.ObjectBuilder;

namespace NServiceBus
{
    public static class ConfigureExtensions
    {
        public static Configure ConfigureInstrumentation(this Configure config)
        {
            return ConfigureInstrumentation(config,new ExeHostInspector());
        }

        public static Configure ConfigureInstrumentation(this Configure config, IHostInspector hostInspector)
        {
            config.Configurer.ConfigureComponent<EndpointMonitor>(ComponentCallModelEnum.Singlecall);
            config.Configurer.ConfigureComponent<MsmqTransportInspector>(ComponentCallModelEnum.Singlecall);

            config.Configurer.RegisterSingleton(typeof (IHostInspector), hostInspector);

            return config;
        }

          public static IBus StartWithInstrumentation(this IStartableBus startableBus)
          {
              var bus = startableBus.Start();
              
              var monitor = Configure.Instance.Builder.Build<IEndpointMonitor>();

              monitor.Start();
              return bus;
          }

    }
}