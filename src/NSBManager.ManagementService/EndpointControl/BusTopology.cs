using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using System.Linq;
using NServiceBus;

namespace NSBManager.ManagementService.EndpointControl
{
    public class BusTopology : IBusTopology
    {
        private readonly IBus bus;

        private IList<Endpoint> knownEndpoints;

        public BusTopology(IBus bus)
        {
            this.bus = bus;
            knownEndpoints = new List<Endpoint>();
        }

        public void EndpointStarted(Endpoint endpoint)
        {
            if (!knownEndpoints.Any(x => x.Id == endpoint.Id))
            {
                endpoint.Status = EndpointStatus.Running;

                knownEndpoints.Add(endpoint);

             
                bus.Publish<IEndpointStartedEvent>(x =>
                {
                    lock (knownEndpoints)
                        x.Endpoints = new List<Endpoint>(knownEndpoints);
                    x.StartedEndpoint = endpoint;
                });
            }
                

        }

      

        public void Initialize(IEnumerable<Endpoint> initialEndpoints)
        {
            knownEndpoints = new List<Endpoint>(initialEndpoints);

            foreach (var endpoint in knownEndpoints)
            {
                endpoint.Status = EndpointStatus.Unknown;
                bus.Send(endpoint.Adress, new EndpointPingRequest());
            }
        }

        public IEnumerable<Endpoint> GetSnapshot()
        {
            lock(knownEndpoints)
                return new List<Endpoint>(knownEndpoints);
        }
    }
}