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
