using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.EndpointControl.MessageHandlers;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;
using System.Collections.Generic;


namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [TestFixture]
    public class When_the_bus_topology_changes
    {
        private IBus bus;
        private string endpointId = "test@testserver";

        private IBusTopology busTopology;

        [SetUp]
        public void Setup()
        {
            bus = MockRepository.GenerateStub<IBus>();
            busTopology = MockRepository.GenerateStub<IBusTopology>();

            busTopology.Stub(x => x.GetCurrentEndpoints()).Return(new List<Endpoint> {new Endpoint
                                                                                          {
                                                                                              Id = endpointId,
                                                                                          }});
            
            IListener<EndpointStartedEvent> messagePublisher =
                new EndpointControlService(bus,busTopology);

            messagePublisher.Handle(new EndpointStartedEvent());
        }

        //[Test]
        //public void A_event_should_be_published_on_the_bus()
        //{
        //    bus.AssertWasPublished<BusTopologyChangedEvent>(p => p.GetType() == typeof(BusTopologyChangedEvent));
        //}

        //[Test]
        //public void The_event_should_contain_the_list_of_known_endpoints()
        //{
        //    bus.AssertWasPublished<BusTopologyChangedEvent>(p => p.Endpoints.Select(e => e.Id == endpointId).Count() == 1);
        //}
    }
}