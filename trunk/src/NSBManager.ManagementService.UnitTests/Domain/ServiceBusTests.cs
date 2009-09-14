using System;
using System.Collections.Generic;
using System.Linq;
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

    public class ServiceBus
    {
        private readonly IList<Endpoint> endpoints = new List<Endpoint>();

        public void RegisterEndpoint(Endpoint endpoint)
        {
            endpoints.Add(endpoint);

        }

        public IEnumerable<Endpoint> Endpoints
        {
            get 
            {
                return endpoints;
            }
        }
    }
}