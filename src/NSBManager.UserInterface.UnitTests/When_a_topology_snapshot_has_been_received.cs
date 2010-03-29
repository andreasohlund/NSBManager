using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.MessageHandlers;
using NSBManager.UserInterface.PhysicalModule.Model;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using System.Linq;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class When_a_topology_snapshot_has_been_received
    {
        [Test]
        public void The_physical_model_should_be_refreshed()
        {
            var physicalModel = MockRepository.GenerateStub<IPhysicalModel>();

            IHandleMessages<TopologySnapshot> messageHandler = new TopologySnapshotMessageHandler(physicalModel);

            var eventMessage = new TopologySnapshot
                                   {
                                       Endpoints = new List<Endpoint> { new Endpoint() }
                                   };
            
            messageHandler.Handle(eventMessage);


            physicalModel.AssertWasCalled(x => x.UpdateModel(Arg<IEnumerable<Endpoint>>.Matches(a => a.Count() == 1)));
        }
    }
} 