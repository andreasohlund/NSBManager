using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    public class TopologySnapshotMessage : IMessage
    {
        public List<Endpoint> Endpoints { get; set; }
    }
}