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
            

            serviceBus.RegisterEndpoint(new Endpoint{Id = Guid.NewGuid()});

            serviceBus.RegisterEndpoint(new Endpoint{Id = Guid.NewGuid()});


            Assert.AreEqual(serviceBus.Endpoints.Count(),2);
        }
    }
}