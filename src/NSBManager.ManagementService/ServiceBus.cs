using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using System.Linq;

namespace NSBManager.ManagementService
{
    public class ServiceBus
    {
        private readonly IList<Endpoint> endpoints = new List<Endpoint>();

        public void RegisterEndpoint(Endpoint endpoint)
        {
            if (endpoints.Count(x => x.Id == endpoint.Id) == 0)
                endpoints.Add(endpoint);

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