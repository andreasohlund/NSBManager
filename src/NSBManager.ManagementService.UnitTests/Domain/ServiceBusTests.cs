using System;
using System.Linq;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.Domain
{
    [TestFixture]
    public class ServiceBusTests
    {
        private ServiceBus serviceBus;

        [SetUp]
        public void Setup()
        {
            var domainEvents = MockRepository.GenerateStub<IDomainEvents>();

            serviceBus = new ServiceBus(domainEvents);

        }

        [Test]
        public void RegisterEndpoint_adds_endpoints()
        {

            

            serviceBus.RegisterEndpoint(new Endpoint{Id = "1@localhost"});

            serviceBus.RegisterEndpoint(new Endpoint{Id = "2@localhost"});


            Assert.AreEqual(serviceBus.Endpoints.Count(),2);

            serviceBus.RegisterEndpoint(new Endpoint { Id = "2@localhost" });

            Assert.AreEqual(serviceBus.Endpoints.Count(), 2);
        }
    }
}