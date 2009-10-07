using System.Collections.Generic;
using NSBManager.ManagementService.Messages;

namespace NSBManager.ManagementService.EndpointControl
{
    public interface IBusTopology
    {
        void RegisterEndpoint(Endpoint endpoint);
        IEnumerable<Endpoint> GetCurrentEndpoints();
    }
}