using System;
using System.Linq;
using NSBManager.Infrastructure;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [TestFixture]
    public class ServiceBusTests
    {
        private BusTopology busTopology;

        private IBus bus;
        [SetUp]
        public void Setup()
        {
            bus = MockRepository.GenerateStub<IBus>();

            busTopology = new BusTopology(bus);

        }

        [Test]
        public void RegisterEndpoint_adds_endpoints()
        {
            var endpoint = new Endpoint
                               {
                                   Id = "1@localhost",
                                   ServerName = "localhost",
                                   AdressOfFailedMessageStore = "error@server"
                               };
            busTopology.RegisterEndpoint(endpoint);

            bus.AssertWasPublished<EndpointStartedEvent>(e => e.AdressOfFailedMessagesStore == endpoint.AdressOfFailedMessageStore);

            busTopology.RegisterEndpoint(new Endpoint{Id = "2@localhost"});


            Assert.AreEqual(busTopology.GetCurrentEndpoints().Count(),2);

            busTopology.RegisterEndpoint(new Endpoint { Id = "2@localhost" });

            Assert.AreEqual(busTopology.GetCurrentEndpoints().Count(), 2);
        }
    }
}