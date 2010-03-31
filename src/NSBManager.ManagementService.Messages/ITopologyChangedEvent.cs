using System.Collections.Generic;
using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    public interface ITopologyChangedEvent:IMessage
    {
        List<Endpoint> Endpoints { get; set; }
    }
}