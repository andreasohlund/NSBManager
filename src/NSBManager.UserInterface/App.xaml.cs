using System.Linq;
using System.Windows;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.StructureMap;
using Microsoft.Practices.ServiceLocation;
using NSBManager.Instrumentation.Core;
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
            ObjectFactory.Profile = "demo";

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

            //ConfigureNServiceBus();

            //var bootStrapper = new Bootstrapper();
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
