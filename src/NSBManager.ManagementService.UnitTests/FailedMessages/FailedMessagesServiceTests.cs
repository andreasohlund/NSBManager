using NSBManager.Infrastructure;
using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.DomainEvents;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.FailedMessages
{
    [TestFixture]
    public class FailedMessagesServiceTests
    {
        private FailedMessagesService service;
        private IFailedMessagesMonitor monitor;
        private IDomainEvents domainEvents;

        [SetUp]
        public void Setup()
        {
            monitor = MockRepository.GenerateStub<IFailedMessagesMonitor>();
            domainEvents = MockRepository.GenerateStub<IDomainEvents>();
            service = new FailedMessagesService(monitor, domainEvents);

        }

        [Test]
        public void Domain_event_should_be_published_when_a_failed_message_is_detected()
        {
            var failedMessage = new FailedMessage();

            monitor.GetEventRaiser(e => e.OnMessageFailed += null).Raise(failedMessage);

            domainEvents.AssertWasCalled(x => x.Publish(Arg<FailedMessageDetectedEvent>.Matches(e=>e.FailedMessage == failedMessage)));
        }
    }
} 