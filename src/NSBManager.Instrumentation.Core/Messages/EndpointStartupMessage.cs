using System;
using NServiceBus;

namespace NSBManager.Instrumentation.Core.Messages
{
    [Serializable]
    public class EndpointStartupMessage : IMessage
    {
        public Guid EndpointId { get; set; }

        public TransportInfo Transport { get; set; }
    }

    public class TransportInfo
    {
        public TransportTypes Type { get; set; }

        public string Adress { get; set; }
    }

    public enum TransportTypes
    {
        Msmq
    }
}