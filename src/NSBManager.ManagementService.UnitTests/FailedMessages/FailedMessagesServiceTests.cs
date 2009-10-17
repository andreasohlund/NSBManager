using System;
using System.Linq;
using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.DomainEvents;
using NUnit.Framework;
using Rhino.Mocks;
using NBehave.Spec.NUnit;
using System.Collections.Generic;

namespace NSBManager.ManagementService.UnitTests.FailedMessages
{
    [TestFixture]
    public class FailedMessagesServiceTests
    {
        private FailedMessagesService service;
        private IFailedMessagesSource source;
        private IDomainEvents domainEvents;
        private IFailedMessagesSourceFactory factory;
        private string adressOfFailedMessagesSource = "error@server";
        [SetUp]
        public void Setup()
        {
            factory = MockRepository.GenerateStub<IFailedMessagesSourceFactory>();
            domainEvents = MockRepository.GenerateStub<IDomainEvents>();
            service = new FailedMessagesService(factory, domainEvents);
            source = MockRepository.GenerateStub<IFailedMessagesSource>();
            source.Stub(x => x.GetAllMessages()).Return(new List<FailedMessage>());
            factory.Stub(x => x.CreateFailedMessagesSource(adressOfFailedMessagesSource)).Return(source);
        }

        [Test]
        public void Domain_event_should_be_published_when_a_failed_message_is_detected()
        {
            var failedMessage = new FailedMessage();

            service.MonitorFailedMessagesSource(adressOfFailedMessagesSource);

            RaiseFailedMessageEvent(failedMessage);

            domainEvents.AssertWasCalled(x => x.Publish(Arg<FailedMessageDetectedEvent>.Matches(e=>e.FailedMessage == failedMessage)));
        }

        [Test]
        public void Failed_messages_should_be_kept_be_the_service()
        {
            var duplicateId = "3";

            service.MonitorFailedMessagesSource(adressOfFailedMessagesSource);


            RaiseFailedMessageEvent(new FailedMessage{Id = "1"});
            RaiseFailedMessageEvent(new FailedMessage { Id = "2" });
            RaiseFailedMessageEvent(new FailedMessage { Id = duplicateId });
            RaiseFailedMessageEvent(new FailedMessage { Id = duplicateId });


            service.FailedMessages.Count().ShouldEqual(3);
        }

        [Test]
        public void Should_start_monitoring_failed_message_sources_when_endpoints_starts_up()
        {
            var handler = (IListener<EndpointStartedEvent>) service;

            var eventMessage = new EndpointStartedEvent { AdressOfFailedMessagesStore = adressOfFailedMessagesSource };

            handler.Handle(eventMessage);

            //mulitple calls with the same adress should be ok
            handler.Handle(eventMessage);

            source.AssertWasCalled(x => x.StartMonitoring());

        }
       
        [Test]
        public void GetAllMessages_should_aggregate_all_message_sources()
        {
            var adressOfAnotherFailedMessagesSource = "124";
            var anotherSource = MockRepository.GenerateStub<IFailedMessagesSource>();

            factory.Stub(x => x.CreateFailedMessagesSource(adressOfAnotherFailedMessagesSource)).Return(anotherSource);

            anotherSource.Stub(x => x.GetAllMessages()).Return(new List<FailedMessage> { new FailedMessage { Id = "2" }, new FailedMessage { Id = "3" } });

            service.MonitorFailedMessagesSource(adressOfFailedMessagesSource);

            service.MonitorFailedMessagesSource(adressOfAnotherFailedMessagesSource);

            service.GetAllMessages().Count().ShouldEqual(2);

        }


        private void RaiseFailedMessageEvent(FailedMessage failedMessage)
        {
            source.GetEventRaiser(e => e.OnMessageFailed += null).Raise(failedMessage);
        }

    }
} 