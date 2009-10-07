using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.FailedMessages.DomainEvents;

namespace NSBManager.ManagementService.FailedMessages
{
    public class FailedMessagesService : IListener<EndpointStartedEvent>
    {
        private readonly IFailedMessagesMonitor monitor;
        private readonly IDomainEvents domainEvents;

        public FailedMessagesService(IFailedMessagesMonitor monitor, 
                                     IDomainEvents domainEvents)
        {
            this.monitor = monitor;
            this.domainEvents = domainEvents;

            monitor.OnMessageFailed += HandleOnMessageFailed;
        }

        private void HandleOnMessageFailed(FailedMessage failedMessage)
        {
            domainEvents.Publish(new FailedMessageDetectedEvent
                                     {
                                         FailedMessage = failedMessage
                                     });
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