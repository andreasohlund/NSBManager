using System;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus;

namespace NSBManager.Instrumentation.Core
{
    public class EndpointMonitor:IEndpointMonitor
    {
        private readonly IBus bus;
        private readonly ITransportInspector transportInspector;

        public EndpointMonitor(IBus bus, ITransportInspector transportInspector)
        {
            this.bus = bus;
            this.transportInspector = transportInspector;
        }

        public void Start()
        {
            var transportInfo = transportInspector.GetTransportInfo();

            var startupMessage = new EndpointStartupMessage
                                     {
                                         EndpointId = transportInfo.Adress,
                                         Server = Environment.MachineName,
                                         Transport = transportInfo
                                     };
            bus.Send(startupMessage);
        }
    }
}