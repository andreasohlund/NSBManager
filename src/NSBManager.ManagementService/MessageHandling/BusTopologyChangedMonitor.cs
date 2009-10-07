using System.Linq;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using NServiceBus.Host;

namespace NSBManager.ManagementService.MessageHandling
{
    public class BusTopologyChangedMonitor : IListener<EndpointStartedEvent>,IWantToRunAtStartup
    {
        private readonly IBus bus;
        private readonly IBusTopology busTopology;

        public BusTopologyChangedMonitor(IBus bus,IBusTopology busTopology)
        {
            this.bus = bus;
            this.busTopology = busTopology;
        }

        public void Handle(EndpointStartedEvent message)
        {
            var eventMessage = new BusTopologyChangedEvent
                                   {
                                       Endpoints = busTopology.GetCurrentEndpoints()
                                       .ToList()
                                   };

            bus.Publish(eventMessage);
        }

        public void Run()
        {
        }

        public void Stop()
        {
        }
    }
}