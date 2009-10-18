using System.Collections.Generic;
using NSBManager.Infrastructure;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.Messages;
using System.Linq;

namespace NSBManager.ManagementService.EndpointControl
{
    public class BusTopology : IBusTopology
    {
        private readonly IDomainEvents domainEvents;

        private readonly IList<Endpoint> endpoints = new List<Endpoint>();

        public BusTopology(IDomainEvents domainEvents)
        {
            this.domainEvents = domainEvents;
        }

        public void RegisterEndpoint(Endpoint endpoint)
        {
            if (endpoints.Count(x => x.Id == endpoint.Id) == 0)
            {
                endpoints.Add(endpoint);


                domainEvents.Publish(new EndpointStartedEvent
                                         {
                                             AdressOfFailedMessagesStore = endpoint.AdressOfFailedMessageStore
                                         });
            }
                

        }

        public IEnumerable<Endpoint> GetCurrentEndpoints()
        {
            return endpoints;
        }
    }
}