using NSBManager.Instrumentation.Messages;
using NServiceBus;

namespace NSBManager.Instrumentation
{
    public class EndpointMonitor
    {
        private readonly IBus bus;

        public EndpointMonitor(IBus bus)
        {
            this.bus = bus;
        }

        public void Start()
        {
            bus.Send(new EndpointStartupMessage());
        }
    }
}