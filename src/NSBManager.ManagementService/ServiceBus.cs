using System.Collections.Generic;
using NSBManager.ManagementService.Messages;

namespace NSBManager.ManagementService
{
    public class ServiceBus
    {
        private readonly IList<Endpoint> endpoints = new List<Endpoint>();

        public void RegisterEndpoint(Endpoint endpoint)
        {
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