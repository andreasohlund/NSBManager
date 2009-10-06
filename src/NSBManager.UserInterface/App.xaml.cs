using System.Linq;
using System.Windows;
using NSBManager.Instrumentation.Core;
using NServiceBus;

namespace NSBManager.UserInterface
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var profile = "";

            if (e.Args.Count() > 0)
                profile = e.Args[0];

            ConfigureNServiceBus();

            var bootStrapper = new Bootstrapper();
            bootStrapper.Run(profile);
        }

        private void ConfigureNServiceBus()
        {
            var config = Configure.With()
                .StructureMapBuilder()
                .BinarySerializer()
                .EnableInstrumentation()
                .UnicastBus()
                    .LoadMessageHandlers()
                        .MsmqTransport()
                        .IsTransactional(true)
                        .PurgeOnStartup(true);

            config.CreateBus()
                .Start();

            var monitor = config.Builder.Build<IEndpointMonitor>();

            monitor.Start();
        }
    }
}
