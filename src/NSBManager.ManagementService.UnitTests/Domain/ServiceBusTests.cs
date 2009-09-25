using System;
using System.Linq;
using NSBManager.ManagementService.Messages;
using NUnit.Framework;

namespace NSBManager.ManagementService.UnitTests.Domain
{
    [TestFixture]
    public class ServiceBusTests
    {
        [Test]
        public void RegisterEndpoint_adds_endpoints()
        {

            var serviceBus = new ServiceBus();
            

            serviceBus.RegisterEndpoint(new Endpoint{Id = "1@localhost"});

            serviceBus.RegisterEndpoint(new Endpoint{Id = "2@localhost"});


            Assert.AreEqual(serviceBus.Endpoints.Count(),2);

            serviceBus.RegisterEndpoint(new Endpoint { Id = "2@localhost" });

            Assert.AreEqual(serviceBus.Endpoints.Count(), 2);
        }
    }
}