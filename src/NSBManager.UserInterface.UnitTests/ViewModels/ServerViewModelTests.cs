using System;
using System.Collections.Generic;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.UserInterface.UnitTests.ViewModels
{
    [TestFixture]
    public class ServerViewModelTests
    {
        private ServerViewModel serverViewModel;

        [SetUp]
        public void Setup()
        {
            var physicalModel = MockRepository.GenerateStub<IPhysicalModel>();

            var endpoints = new List<NSBManager.ManagementService.Messages.Endpoint>
                                {
                                    new ManagementService.Messages.Endpoint {Id = "endpoint1",ServerName ="server1"},
                                    new ManagementService.Messages.Endpoint {Id = "endpoint2",ServerName ="server1"},
                                    new ManagementService.Messages.Endpoint {Id = "endpoint3",ServerName ="server2"},
                                    
                                };
            physicalModel.Stub(x => x.Endpoints).Return(endpoints);
            serverViewModel = new ServerViewModel(physicalModel);
        }

        [Test]
        public void Shall_contain_a_list_of_server_names()
        {
            Assert.That(serverViewModel.Servers.Count > 0);
        }
    }
}