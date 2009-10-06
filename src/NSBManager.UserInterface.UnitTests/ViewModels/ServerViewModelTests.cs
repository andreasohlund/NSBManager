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

            var endpoints = new List<Endpoint>
                                {
                                    new Endpoint {Name = "endpoint1", ServerName = "server1"},
                                    new Endpoint {Name = "endpoint2", ServerName = "server1"},
                                    new Endpoint {Name = "endpoint1", ServerName = "server2"},
                                    new Endpoint {Name = "endpoint1", ServerName = "server3"},
                                    new Endpoint {Name = "endpoint2", ServerName = "server3"},
                                    new Endpoint {Name = "endpoint1", ServerName = "server4"}
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