using System;
using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    [Serializable]
    public class EndpointStartedEvent:IMessage
    {
        public string AdressOfFailedMessagesStore { get; set; }
    }
}