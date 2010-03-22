using System;
using System.Collections.Generic;
using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    [Serializable]
    public class TopologySnapshotMessage : IMessage
    {
        public List<Endpoint> Endpoints { get; set; }
    }
}