using System;
using System.Linq;
using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.DomainEvents;
using NServiceBus.Host;
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
        private IFailedMessagesStore store;
        private IDomainEvents domainEvents;
        private IFailedMessagesStoreFactory factory;
        private string adressOfFailedMessagesStore = "error@server";
        [SetUp]
        public void Setup()
        {
            factory = MockRepository.GenerateStub<IFailedMessagesStoreFactory>();
            domainEvents = MockRepository.GenerateStub<IDomainEvents>();
            service = new FailedMessagesService(factory, domainEvents);
            store = MockRepository.GenerateStub<IFailedMessagesStore>();
            store.Stub(x => x.GetAllMessages()).Return(new List<FailedMessage>());
            factory.Stub(x => x.CreateFailedMessagesStore(adressOfFailedMessagesStore)).Return(store);
        }

        [Test]
        public void Domain_event_should_be_published_when_a_failed_message_is_detected()
        {
            var failedMessage = new FailedMessage();

            service.MonitorFailedMessagesStores(adressOfFailedMessagesStore);

            RaiseFailedMessageEvent(failedMessage);

            domainEvents.AssertWasCalled(x => x.Publish(Arg<FailedMessageDetectedEvent>.Matches(e=>e.FailedMessage == failedMessage)));
        }

        [Test]
        public void Failed_messages_should_be_kept_be_the_service()
        {
            var duplicateId = "3";

            service.MonitorFailedMessagesStores(adressOfFailedMessagesStore);


            RaiseFailedMessageEvent(new FailedMessage{Id = "1"});
            RaiseFailedMessageEvent(new FailedMessage { Id = "2" });
            RaiseFailedMessageEvent(new FailedMessage { Id = duplicateId });
            RaiseFailedMessageEvent(new FailedMessage { Id = duplicateId });


            service.FailedMessages.Count().ShouldEqual(3);
        }

        [Test]
        public void Should_start_monitoring_failed_message_store_when_endpoints_starts_up()
        {
            var handler = (IListener<EndpointStartedEvent>) service;

            var eventMessage = new EndpointStartedEvent { AdressOfFailedMessagesStore = adressOfFailedMessagesStore };

            handler.Handle(eventMessage);

            //mulitple calls with the same adress should be ok
            handler.Handle(eventMessage);

            store.AssertWasCalled(x => x.StartMonitoring());

        }
       
        [Test]
        public void GetAllMessages_should_aggregate_all_message_stores()
        {
            var adressOfAnotherFailedMessagesStore = "124";
            var anotherStore = MockRepository.GenerateStub<IFailedMessagesStore>();

            factory.Stub(x => x.CreateFailedMessagesStore(adressOfAnotherFailedMessagesStore)).Return(anotherStore);

            anotherStore.Stub(x => x.GetAllMessages()).Return(new List<FailedMessage> { new FailedMessage { Id = "2" }, new FailedMessage { Id = "3" } });

            service.MonitorFailedMessagesStores(adressOfFailedMessagesStore);

            service.MonitorFailedMessagesStores(adressOfAnotherFailedMessagesStore);

            service.GetAllMessages().Count().ShouldEqual(2);

        }

        [Test]
        public void Should_run_at_startup()
        {
            (service as IWantToRunAtStartup).ShouldNotBeNull();
        }

        [Test]
        public void Should_enable_resubmission_of_messages()
        {
            var failedMessage = new FailedMessage
                                    {
                                        Id = "1",
                                        Origin = "queue@server",
                                        AddressOfFailedMessageStore = adressOfFailedMessagesStore
                                    };

            service.MonitorFailedMessagesStores(adressOfFailedMessagesStore);

            RaiseFailedMessageEvent(failedMessage);


            service.RetryMessage("1");

            store.AssertWasCalled(x => x.RetryMessage(failedMessage));

            Assert.Throws<Exception>(()=>service.RetryMessage("bad id"));
            
        }


        private void RaiseFailedMessageEvent(FailedMessage failedMessage)
        {
            store.GetEventRaiser(e => e.OnMessageFailed += null).Raise(failedMessage);
        }

    }
} 