using System;
using System.Collections.Generic;
using NSBManager.ManagementService.UnitTests.Domain;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;

namespace NSBManager.ManagementService.UnitTests
{
    [TestFixture]
    public class When_a_endpointstartup_message_is_received
    {
        private IBus bus;
        private Guid endpointId;


        [SetUp]
        public void SetUp()
        {
            bus = MockRepository.GenerateStub<IBus>();
            var serviceBus = new ServiceBus();

            IHandleMessages<EndpointStartupMessage> messageHandler = new EndpointStartupMessageHandler(bus,serviceBus);

            endpointId = Guid.NewGuid();


            var endpointStartupMessage = new EndpointStartupMessage
            {
                EndpointId = endpointId
            };


            messageHandler.Handle(endpointStartupMessage);

        }

        [Test]
        public void A_busTopologyChangedEvent_event_should_be_published()
        {
            bus.AssertWasPublished<BusTopologyChangedEvent>(p => p.GetType() == typeof(BusTopologyChangedEvent));
        }

        [Test]
        public void The_event_should_contain_a_list_of_endpoints()
        {
            bus.AssertWasPublished<BusTopologyChangedEvent>(p => p.Endpoints.Select(e=>e.Id == endpointId).Count()==1);
        }



    }

    public class BusTopologyChangedEvent : IMessage
    {
        public IEnumerable<Endpoint> Endpoints { get; set; }

    }

    public class Endpoint
    {

        public Guid Id { get; set; }
    }

    public class EndpointStartupMessageHandler:IHandleMessages<EndpointStartupMessage>
    {
        private readonly IBus bus;
        private readonly ServiceBus serviceBus;

        public EndpointStartupMessageHandler(IBus bus,ServiceBus serviceBus)
        {
            this.bus = bus;
            this.serviceBus = serviceBus;
        }

        public void Handle(EndpointStartupMessage message)
        {
            var endpoint = new Endpoint {Id = message.EndpointId};
            serviceBus.RegisterEndpoint(endpoint);

            var busTopologyChangedEvent = new BusTopologyChangedEvent
                                              {
                                                  Endpoints = serviceBus.Endpoints
                                              };

            bus.Publish(busTopologyChangedEvent);
        }
    }

    public class EndpointStartupMessage : IMessage
    {
        public Guid EndpointId { get; set; }
    }
}