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

            bootStrapper.CreateShell();

            //this will of course change when we starts to use the shell
            //var view = ObjectFactory.GetInstance<EndpointListView>();
            
            //view.Show();
        }



       
        private void ConfigureNServiceBus()
        {
            bus = Configure.With()
                .StructureMapBuilder()
                .XmlSerializer()
                .UnicastBus()
                    .LoadMessageHandlers()
                        .MsmqTransport()
                        .IsTransactional(true)
                        .PurgeOnStartup(true)
                .CreateBus()
                .Start();


          

            var monitor = new EndpointMonitor(bus);

           monitor.Start();
        }
    }
}
