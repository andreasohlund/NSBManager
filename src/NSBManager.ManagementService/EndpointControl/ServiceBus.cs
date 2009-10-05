using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using System.Linq;

namespace NSBManager.ManagementService.EndpointControl
{
    public class ServiceBus : IServiceBus
    {
        private readonly IDomainEvents domainEvents;

        private readonly IList<Endpoint> endpoints = new List<Endpoint>();

        public ServiceBus(IDomainEvents domainEvents)
        {
            this.domainEvents = domainEvents;
        }

        public void RegisterEndpoint(Endpoint endpoint)
        {
            if (endpoints.Count(x => x.Id == endpoint.Id) == 0)
            {
                endpoints.Add(endpoint);

               
               //domainEvents.Publish(new EndpointStartedEvent)
            }
                

        }

        public IEnumerable<Endpoint> Endpoints
        {
            get 
            {
                return endpoints;
            }
        }
    }
}