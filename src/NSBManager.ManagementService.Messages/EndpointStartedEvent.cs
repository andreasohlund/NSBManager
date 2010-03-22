using System;
using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    [Serializable]
    public class EndpointStartedEvent:IMessage
    {
        public Endpoint Endpoint { get; set; }
    }
}