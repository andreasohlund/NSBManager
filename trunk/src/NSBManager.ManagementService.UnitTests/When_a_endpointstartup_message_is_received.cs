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
        public void A_busTopologyChangedEvent_event_should_be_published()
        {
            var bus = MockRepository.GenerateStub<IBus>();

            IHandleMessages<EndpointStartupMessage> messageHandler = new EndpointStartupMessageHandler(bus);

            var endpointStartupMessage = new EndpointStartupMessage
                                             {
                                                 EndpointId = Guid.NewGuid()
                                             };


            messageHandler.Handle(endpointStartupMessage);

            bus.AssertWasPublished<BusTopologyChangedEvent>(p => p.GetType() == typeof(BusTopologyChangedEvent));
        }
    }

    public class BusTopologyChangedEvent : IMessage
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
            bus.Publish(new BusTopologyChangedEvent());
        }
}

    public class EndpointStartupMessage : IMessage
    {
        public Guid EndpointId { get; set; }
    }
}