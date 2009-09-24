using NSBManager.Instrumentation.Core.Messages;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using System.Linq;

namespace NSBManager.ManagementService
{
    public class EndpointStartupMessageHandler:IHandleMessages<EndpointStartupMessage>
    {
        private readonly IBus bus;
        private readonly ServiceBus serviceBus;

        public EndpointStartupMessageHandler(IBus bus,ServiceBus serviceBus)
        {
            this.bus = bus;
            this.serviceBus = serviceBus;
        }

        public void Handle(EndpointStartupMessage message)
        {
            var endpoint = new Endpoint {Id = message.EndpointId};
            serviceBus.RegisterEndpoint(endpoint);

            var busTopologyChangedEvent = new BusTopologyChangedEvent
                                              {
                                                  Endpoints = serviceBus.Endpoints.ToList()
                                              };

            bus.Publish(busTopologyChangedEvent);
        }
    }
}