using System.Collections.Generic;
using System.Linq;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.Model;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.UserInterface.UnitTests.Models
{
    [TestFixture]
    public class PhysicalModelExtensionTests
    {
        private IPhysicalModel physicalModel;

        [SetUp]
        public void SetUp()
        {
            physicalModel = MockRepository.GenerateMock<IPhysicalModel>();
            
            var endpoints = GenerateListOfEndpoints();

            physicalModel.Stub(x => x.Endpoints).Return(endpoints);
        }

        [Test]
        public void EndpointsOnServer_shall_return_a_list_of_GuiEndpoints()
        {
            var endpointsOnServer = physicalModel.EndpointsOnServer("Server1").ToList();

            Assert.That(endpointsOnServer.Count, Is.EqualTo(3));
        }

        [Test]
        public void Servers_shall_return_a_list_of_servers()
        {
            var servers = physicalModel.Servers().ToList();

            Assert.That(servers.Count, Is.EqualTo(2));
        }

        private IList<Endpoint> GenerateListOfEndpoints()
        {
            return new List<Endpoint>
                       {
                           new Endpoint {Id = "Endpoint1", ServerName = "Server1"},
                           new Endpoint {Id = "Endpoint2", ServerName = "Server1"},
                           new Endpoint {Id = "Endpoint3", ServerName = "Server1"},
                           new Endpoint {Id = "Endpoint1", ServerName = "Server2"}
                       };
        }
    }
}