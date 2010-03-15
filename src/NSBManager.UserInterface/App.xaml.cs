using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NSBManager.Instrumentation.Core;
using NServiceBus;

namespace NSBManager.UserInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
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
            //bootStrapper.Run(profile);
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
