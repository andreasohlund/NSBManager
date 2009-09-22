using NSBManager.ManagementService.Messages;
using NServiceBus;
using NUnit.Framework;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class When_the_bus_topology_changes
    {
        [Test,Ignore("TODO")]
        public void The_physical_view_should_be_updated()
        {

            IHandleMessages<BusTopologyChangedEvent> messageHandler = new BusTopologyChangedEventHandler();


            var eventMessage = new BusTopologyChangedEvent
                                   {
                                       Endpoints = new System.Collections.Generic.List<NSBManager.ManagementService.Messages.Endpoint> { new ManagementService.Messages.Endpoint() }
                                   };
            
            messageHandler.Handle(eventMessage);



        }
    }

    public class BusTopologyChangedEventHandler : IHandleMessages<BusTopologyChangedEvent>
    {
        public void Handle(BusTopologyChangedEvent message)
        {
            throw new System.NotImplementedException();
        }
    }
} 