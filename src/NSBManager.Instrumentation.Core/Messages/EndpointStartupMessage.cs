using System;
using NServiceBus;

namespace NSBManager.Instrumentation.Core.Messages
{
    [Serializable]
    public class EndpointStartupMessage : IMessage
    {
        public string EndpointId { get; set; }

        public string Server { get; set; }

        public TransportInfo Transport { get; set; }

        public HostInfo Host { get; set; }
    }

    [Serializable]
    public class TransportInfo
    {
        public string Adress { get; set; }
    }

}