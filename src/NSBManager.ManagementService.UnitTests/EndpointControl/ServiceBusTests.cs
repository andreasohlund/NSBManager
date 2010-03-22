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
            busTopology.EndpointStarted(endpoint);

            bus.AssertWasPublished<EndpointStartedEvent>(e => e.Endpoint.AdressOfFailedMessageStore == endpoint.AdressOfFailedMessageStore);

            busTopology.EndpointStarted(new Endpoint{Id = "2@localhost"});


            Assert.AreEqual(busTopology.GetSnapshot().Count(),2);

            busTopology.EndpointStarted(new Endpoint { Id = "2@localhost" });

            Assert.AreEqual(busTopology.GetSnapshot().Count(), 2);
        }
    }
}