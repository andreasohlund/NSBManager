using System;
using System.Collections.Generic;
using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    [Serializable]
    public class BusTopologyChangedEvent : IMessage
    {
        public IEnumerable<Endpoint> Endpoints { get; set; }

    }
}