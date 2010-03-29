using System;
using System.Collections.Generic;
using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    [Serializable]
    public class TopologySnapshot : IMessage
    {
        public List<Endpoint> Endpoints { get; set; }
    }
}