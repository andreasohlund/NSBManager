using System.Collections.Generic;
using NSBManager.ManagementService.Messages;

namespace NSBManager.ManagementService.EndpointControl
{
    public interface IBusTopology
    {
        void EndpointStarted(Endpoint endpoint);
        IEnumerable<Endpoint> GetCurrentEndpoints();
        void Initialize(IEnumerable<Endpoint> knownEndpoints);
    }
}