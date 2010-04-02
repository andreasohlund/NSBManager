using System;
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
            ConnectToNServiceBus();
            base.BeforeConfiguration();
        }

        protected override IServiceLocator CreateContainer()
        {
            ObjectFactory.Configure(x => x.AddRegistry<UserInterfaceRegistry>());

            var commandLine = Environment.CommandLine.Split(' ');

            if (commandLine.Length > 1)
                ObjectFactory.Profile = commandLine[1];

            return new StructureMapAdapter(ObjectFactory.Container);
        }

        protected override object CreateRootModel()
        {
            return Container.GetInstance<IShell>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);

            //todo: This should be done using a dialog that asks the user what service to connect to
            // and the use that adress to configure where so send the connect request
            ConnectToNServiceBus();

            //todo: move this to a separate screen
            bus.Send(new ClientConnectRequest());
        }

        private static void ConnectToNServiceBus()
        {
            bus = Configure.With()
                .Log4Net(new ConsoleAppender { Threshold = Level.Info })
                .StructureMapBuilder()
                .BinarySerializer()
                .ConfigureInstrumentation()
                .MsmqTransport()
                    .IsTransactional(true)
                    .PurgeOnStartup(true)
                .UnicastBus()
                    .LoadMessageHandlers()
                .CreateBus()
                .StartWithInstrumentation();

        }

        static IBus bus;
    }
}
