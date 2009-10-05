using System;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.FailedMessages
{
    [TestFixture]
    public class When_a_endpoint_has_started
    {
        [Test]
        public void Its_failed_messages_store_should_be_monitored()
        {
            var monitor = MockRepository.GenerateStub<IFailedMessagesMonitor>();

            IListener<EndpointStartedEvent> service = new FailedMessagesService(monitor);

            var eventMessage = new EndpointStartedEvent {AdressOfFailedMessagesStore = "error@someserver"};

            service.Handle(eventMessage);

            monitor.AssertWasCalled(x => x.StartMonitoring(eventMessage.AdressOfFailedMessagesStore));
        }
    }

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

    public interface IFailedMessagesMonitor
    {
        void StartMonitoring(string adress);
    }
} 