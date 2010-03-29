using System.Linq;
using System.Windows;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.StructureMap;
using log4net.Appender;
using log4net.Core;
using Microsoft.Practices.ServiceLocation;
using NSBManager.Instrumentation.Core;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.ViewModels;
using NServiceBus;
using StructureMap;

namespace NSBManager.UserInterface
{
    public partial class App
    {
        protected override void BeforeConfiguration()
        {
            ConfigureNServiceBus();
            base.BeforeConfiguration();
        }

        protected override IServiceLocator CreateContainer()
        {
            ObjectFactory.Configure(x => x.AddRegistry<UserInterfaceRegistry>());

            //Todo: Change later
            //ObjectFactory.Profile = "demo";

            return new StructureMapAdapter(ObjectFactory.Container);
        }

        protected override object CreateRootModel()
        {
            return Container.GetInstance<IShell>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            //Todo: This runs after CreateContainer, so how shall we get the profile?
            base.OnStartup(e);

            var profile = "";

            if (e.Args.Count() > 0)
                profile = e.Args[0];

            
            //todo: This should be done using a dialog that asks the user what service to connect to
            // and the use that adress to configure where so send the connect request
            ConfigureNServiceBus();
            bus.Send(new ClientConnectRequest());
        }

        private static void ConfigureNServiceBus()
        {
            var config = Configure.With()
                .Log4Net(new ConsoleAppender{Threshold = Level.Info})
                .StructureMapBuilder()
                .BinarySerializer()
                .EnableInstrumentation()
                .MsmqTransport()
                    .IsTransactional(true)
                    .PurgeOnStartup(true)
                .UnicastBus()
                    .LoadMessageHandlers();
                    
            bus = config.CreateBus()
                .Start();


            //todo use the NSB startup event to do this without requireing the user to explicilty start the monitor
            var monitor = config.Builder.Build<IEndpointMonitor>();

            monitor.Start();
        }

        static IBus bus;
    }
}
