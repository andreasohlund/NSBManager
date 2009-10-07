using System;
using System.Linq;
using NSBManager.Infrastructure;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.Messages;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.Domain
{
    [TestFixture]
    public class ServiceBusTests
    {
        private BusTopology busTopology;

        private IDomainEvents domainEvents;
        [SetUp]
        public void Setup()
        {
            domainEvents = MockRepository.GenerateStub<IDomainEvents>();

            busTopology = new BusTopology(domainEvents);

        }

        [Test]
        public void RegisterEndpoint_adds_endpoints()
        {
            busTopology.RegisterEndpoint(new Endpoint{Id = "1@localhost"});

            domainEvents.AssertWasCalled(x => x.Publish(Arg<EndpointStartedEvent>.Is.Anything));

            busTopology.RegisterEndpoint(new Endpoint{Id = "2@localhost"});


            Assert.AreEqual(busTopology.GetCurrentEndpoints().Count(),2);

            busTopology.RegisterEndpoint(new Endpoint { Id = "2@localhost" });

            Assert.AreEqual(busTopology.GetCurrentEndpoints().Count(), 2);
        }
    }
}