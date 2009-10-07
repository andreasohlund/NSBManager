using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

namespace NSBManager.UserInterface.UnitTests.ViewModels
{
    [TestFixture]
    public class ServerDetailsViewModelTests
    {
        private ServerDetailsViewModel serverViewModel;

        [SetUp]
        public void SetUp()
        {
            var physicalModel = MockRepository.GenerateStub<IPhysicalModel>();

            var endpoints = new List<NSBManager.ManagementService.Messages.Endpoint>
                                {
                                    new ManagementService.Messages.Endpoint {Id = "endpoint1",ServerName ="server1"},
                                    new ManagementService.Messages.Endpoint {Id = "endpoint2",ServerName ="server1"},
                                    new ManagementService.Messages.Endpoint {Id = "endpoint3",ServerName ="server2"},
                                    
                                };

            physicalModel.Stub(x => x.Endpoints).Return(endpoints);
            serverViewModel = new ServerDetailsViewModel(physicalModel)
                                  {
                                      Filter = "server1"
                                  };
        }

        [Test]
        public void Shall_contain_endpoints()
        {
            Assert.That(serverViewModel.Endpoints.Count.Equals(2));
        }
    }
}