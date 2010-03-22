using System.Linq;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.ManagementService.EndpointControl.MessageHandlers
{
    public class EndpointControlService :   IWantToRunAtStartup,
                                            IListener<EndpointStartedEvent>
    {
        private readonly IBus bus;
        private readonly IBusTopology busTopology;

        public EndpointControlService(IBus bus,IBusTopology busTopology)
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
            //TODO: load endpoints from store
            busTopology.Initialize(null);
        }

        public void Stop()
        {
        }
    }
}