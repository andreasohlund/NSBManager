using System.Collections.Generic;
using System.Linq;
using NSBManager.ManagementService.Messages;
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
            busTopology.Initialize(new List<Endpoint>());
        }

        public void Stop()
        {
        }

        public void Handle(ClientConnectRequest request)
        {
         
           bus.Reply(new TopologySnapshot
                         {
                             Endpoints=busTopology.GetSnapshot().ToList()
                         }); 
        }
    }
}