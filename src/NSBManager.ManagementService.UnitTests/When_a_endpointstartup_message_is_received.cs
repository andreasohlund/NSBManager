using NSBManager.Instrumentation.Core.Messages;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.MessageHandling;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests
{
    [TestFixture]
    public class When_a_endpointstartup_message_is_received
    {
        private string endpointId;
        private IServiceBus serviceBus;


        [SetUp]
        public void SetUp()
        {
            serviceBus = MockRepository.GenerateStub<IServiceBus>();

            IHandleMessages<EndpointStartupMessage> messageHandler = new EndpointStartupMessageHandler(serviceBus);

            endpointId = "test@localhost";


            var endpointStartupMessage = new EndpointStartupMessage
            {
                EndpointId = endpointId
            };


            messageHandler.Handle(endpointStartupMessage);

        }

        [Test]
        public void The_servicebus_should_be_notified()
        {
            serviceBus.AssertWasCalled(b => b.RegisterEndpoint(Arg<Endpoint>.Matches(e => e.Id == endpointId)));
        }

        //[Test]
        //public void A_busTopologyChangedEvent_event_should_be_published()
        //{
        //    bus.AssertWasPublished<BusTopologyChangedEvent>(p => p.GetType() == typeof(BusTopologyChangedEvent));
        //}

        //[Test]
        //public void The_event_should_contain_a_list_of_endpoints()
        //{
        //    bus.AssertWasPublished<BusTopologyChangedEvent>(p => p.Endpoints.Select(e=>e.Id == endpointId).Count()==1);
        //}



    }
}