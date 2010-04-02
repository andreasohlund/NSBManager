using System;
using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus;

namespace NSBManager.Instrumentation.Core
{
    public class EndpointMonitor:IEndpointMonitor
    {
        private readonly IBus bus;
        private readonly ITransportInspector transportInspector;
        private readonly IHostInspector hostInspector;

        public EndpointMonitor( IBus bus,
                                ITransportInspector transportInspector, 
                                IHostInspector hostInspector)
        {
            this.bus = bus;
            this.transportInspector = transportInspector;
            this.hostInspector = hostInspector;
        }

        public void Start()
        {
            var transportInfo = transportInspector.GetTransportInformation();

            var startupMessage = new EndpointStartupMessage
                                     {
                                         EndpointId = transportInfo.Adress,
                                         Server = Environment.MachineName,
                                         Transport = transportInfo,
                                         Host = hostInspector.GetHostInformation()
                                     };
            bus.Send(startupMessage);
        }
    }
}