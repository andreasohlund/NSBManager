﻿using System.Linq;
using System.Windows;
using NSBManager.Instrumentation.Core;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.Views;
using NServiceBus;
using StructureMap;

namespace NSBManager.UserInterface
{
    public partial class App
    {
        private IBus bus;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootStrapper = new Bootstrapper();

            var profile = "";

            if (e.Args.Count() > 0)
                profile = e.Args[0];

            bootStrapper.BootstrapStructureMap(profile);

            ConfigureNServiceBus();

            
            bootStrapper.Run();
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

            bus = config.CreateBus()
                .Start();


            var monitor = config.Builder.Build<IEndpointMonitor>();

            monitor.Start();
        }
    }
}
