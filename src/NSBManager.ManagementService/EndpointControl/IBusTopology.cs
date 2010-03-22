using System.Collections.Generic;
using NSBManager.ManagementService.Messages;

namespace NSBManager.ManagementService.EndpointControl
{
    public interface IBusTopology
    {
        void EndpointStarted(Endpoint endpoint);
        void Initialize(IEnumerable<Endpoint> knownEndpoints);
        IEnumerable<Endpoint> GetSnapshot();
    }
}