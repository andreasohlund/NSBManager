using System;
using NSBManager.Instrumentation;
using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.UserInterface.Views
{
    /// <summary>
    /// Interaction logic for EndpointListView.xaml
    /// </summary>
    public partial class EndpointListView
    {
        private IBus bus;

        public EndpointListView()
        {
            ConfigureNServiceBus();

            InitializeComponent();
        }

        private void ConfigureNServiceBus()
        {
            bus = Configure.With()
                .SpringBuilder()
                .BinarySerializer()
                .UnicastBus()
                    .MsmqTransport()
                    .CreateBus()
                    .Start();


            bus.Subscribe<BusTopologyChangedEvent>();
          

            var monitor = new EndpointMonitor(bus);

            monitor.Start();


        }
    }
}
