using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl.DomainEvents;

namespace NSBManager.ManagementService.FailedMessages
{
    public class FailedMessagesService : IListener<EndpointStartedEvent>
    {
        private readonly IFailedMessagesMonitor monitor;

        public FailedMessagesService(IFailedMessagesMonitor monitor)
        {
            this.monitor = monitor;
        }

        public void Handle(EndpointStartedEvent message)
        {
            MonitorFailedMessagesStore(message.AdressOfFailedMessagesStore);
        }

        private void MonitorFailedMessagesStore(string adressOfFailedMessagesStore)
        {
            monitor.StartMonitoring(adressOfFailedMessagesStore);
        }
    }
}