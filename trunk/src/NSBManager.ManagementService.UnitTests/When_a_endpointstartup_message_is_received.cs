using System;
using NSBManager.Instrumentation.Core.Messages;
using NSBManager.ManagementService.Messages;
using NSBManager.TestHelpers;
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
}