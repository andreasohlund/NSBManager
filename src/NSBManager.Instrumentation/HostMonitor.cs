using System;
using NSBManager.Instrumentation.Core;
using NServiceBus.Host;

namespace NSBManager.Instrumentation
{
    public class HostMonitor : IWantToRunAtStartup
    {
        private readonly IEndpointMonitor endpointMonitor;

        public HostMonitor(IEndpointMonitor endpointMonitor)
        {
            this.endpointMonitor = endpointMonitor;
        }

        public void Run()
        {
            endpointMonitor.Start();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}