using System;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus;

namespace NSBManager.Instrumentation.Core
{
    public class EndpointMonitor:IEndpointMonitor
    {
        private readonly IBus bus;
        private readonly Guid endpointId;
        private readonly ITransportInspector transportInspector;

        public EndpointMonitor(IBus bus, ITransportInspector transportInspector)
        {
            endpointId = Guid.NewGuid();

            this.bus = bus;
            this.transportInspector = transportInspector;
        }

        public void Start()
        {
            var startupMessage = new EndpointStartupMessage
                                     {
                                         EndpointId = endpointId,
                                         Transport = transportInspector.GetTransportInfo()
                                     };
            bus.Send(startupMessage);
        }
    }
}