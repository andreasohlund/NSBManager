using System;
using System.Linq;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.Messages;
using NSBManager.ManagementService.UnitTests.EndpointControl;
using NServiceBus;

namespace NSBManager.ManagementService.EndpointControl
{
    public class EndpointControlService :   IWantToRunAtStartup,
                                            IHandleMessages<ClientConnectRequest> 

    {
        private readonly IBus bus;
        private readonly IBusTopology busTopology;

        public EndpointControlService(IBus bus,IBusTopology busTopology)
        {
            this.bus = bus;
            this.busTopology = busTopology;
        }

        public void Run()
        {
            //TODO: load endpoints from store
            busTopology.Initialize(null);
        }

        public void Stop()
        {
        }

        public void Handle(ClientConnectRequest request)
        {
         
           bus.Reply(new TopologySnapshotMessage
                         {
                             Endpoints=busTopology.GetSnapshot().ToList()
                         }); 
        }
    }
}