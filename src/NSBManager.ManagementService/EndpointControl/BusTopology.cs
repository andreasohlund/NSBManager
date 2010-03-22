using System;
using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using System.Linq;
using NServiceBus;

namespace NSBManager.ManagementService.EndpointControl
{
    public class BusTopology : IBusTopology
    {
        private readonly IBus bus;

        private IList<Endpoint> endpoints;

        public BusTopology(IBus bus)
        {
            this.bus = bus;
            endpoints = new List<Endpoint>();
        }

        public void EndpointStarted(Endpoint endpoint)
        {
            if (!endpoints.Any(x => x.Id == endpoint.Id))
            {
                endpoint.Status = EndpointStatus.Running;

                endpoints.Add(endpoint);


                bus.Publish(new EndpointStartedEvent
                                         {
                                             Endpoint = endpoint
                                         });
            }
                

        }

      

        public void Initialize(IEnumerable<Endpoint> initialEndpoints)
        {
            endpoints = new List<Endpoint>(initialEndpoints);

            foreach (var endpoint in endpoints)
            {
                endpoint.Status = EndpointStatus.Unknown;
                bus.Send(endpoint.Adress, new EndpointPingRequest());
            }
        }

        public IEnumerable<Endpoint> GetSnapshot()
        {
            lock(endpoints)
                return new List<Endpoint>(endpoints);
        }
    }
}