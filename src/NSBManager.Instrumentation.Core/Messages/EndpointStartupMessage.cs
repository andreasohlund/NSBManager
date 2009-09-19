using System;
using NServiceBus;

namespace NSBManager.Instrumentation.Core.Messages
{
    [Serializable]
    public class EndpointStartupMessage : IMessage
    {
        public Guid EndpointId { get; set; }
    }
}