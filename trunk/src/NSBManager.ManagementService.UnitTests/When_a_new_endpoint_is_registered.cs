using System;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests
{
    [TestFixture]
    public class When_a_endpointstartup_message_is_received
    {
        [Test]
        public void A_endpointlistupdated_event_should_be_published()
        {
            var bus = MockRepository.GenerateStub<IBus>();

            IHandleMessages<EndpointStartupMessage> messageHandler = new EndpointStartupMessageHandler(bus);

            var endpointStartupMessage = new EndpointStartupMessage
                                             {
                                                 EndpointId = Guid.NewGuid()
                                             };


            messageHandler.Handle(endpointStartupMessage);

            bus.AssertWasPublished<EndpointListUpdatedEvent>(p => p.GetType() == typeof(EndpointListUpdatedEvent));
        }
    }

    public class EndpointListUpdatedEvent : IMessage
    {
    }

    public class EndpointStartupMessageHandler:IHandleMessages<EndpointStartupMessage>

{
        private readonly IBus bus;

        public EndpointStartupMessageHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(EndpointStartupMessage message)
        {
            bus.Publish(new EndpointListUpdatedEvent());
        }
}

    public class EndpointStartupMessage : IMessage
    {
        public Guid EndpointId { get; set; }
    }
}