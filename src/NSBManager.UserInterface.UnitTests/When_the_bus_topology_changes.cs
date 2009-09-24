using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.MessageHandlers;
using NSBManager.UserInterface.PhysicalModule.Model;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using Endpoint=NSBManager.ManagementService.Messages.Endpoint;
using System.Linq;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class When_the_bus_topology_changes
    {
        [Test]
        public void The_physical_model_should_be_updated()
        {
            var physicalModel = MockRepository.GenerateStub<IPhysicalModel>();

            IHandleMessages<BusTopologyChangedEvent> messageHandler = new BusTopologyChangedEventHandler(physicalModel,null);

            var eventMessage = new BusTopologyChangedEvent
                                   {
                                       Endpoints = new List<Endpoint> { new Endpoint() }
                                   };
            
            messageHandler.Handle(eventMessage);


            physicalModel.AssertWasCalled(x => x.UpdateModel(Arg<IEnumerable<UserInterface.Models.Endpoint>>.Matches(a => a.Count() == 1)));
        }
    }
} 