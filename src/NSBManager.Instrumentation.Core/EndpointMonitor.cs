using System;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus;

namespace NSBManager.Instrumentation.Core
{
    public class EndpointMonitor:IEndpointMonitor
    {
        private readonly IBus bus;
        private readonly Guid endpointId;

        public EndpointMonitor(IBus bus)
        {
            endpointId = Guid.NewGuid();

            this.bus = bus;
        }

        public void Start()
        {
            bus.Send(new EndpointStartupMessage { EndpointId = endpointId });
        }
    }
}