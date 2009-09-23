using System.Linq;
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

            if (e.Args.Count() > 1)
                profile = e.Args[0];

            bootStrapper.BootstrapStructureMap(profile);

            ConfigureNServiceBus();

            var view = ObjectFactory.GetInstance<EndpointListView>();

            view.Show();
        }



       
        private void ConfigureNServiceBus()
        {
            bus = Configure.With()
                .StructureMapBuilder()
                .XmlSerializer()
                .UnicastBus()
                    .MsmqTransport()
                    .PurgeOnStartup(true)
                .CreateBus()
                .Start();


            bus.Subscribe<BusTopologyChangedEvent>();
          

            var monitor = new EndpointMonitor(bus);

            monitor.Start();
        }
    }
}
