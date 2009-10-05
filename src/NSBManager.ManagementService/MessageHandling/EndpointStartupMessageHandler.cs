using NSBManager.Instrumentation.Core.Messages;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.ManagementService.MessageHandling
{
    public class EndpointStartupMessageHandler:IHandleMessages<EndpointStartupMessage>
    {
        private readonly IServiceBus serviceBus;

        public EndpointStartupMessageHandler(IServiceBus serviceBus)
        {
            this.serviceBus = serviceBus;
        }

        public void Handle(EndpointStartupMessage message)
        {
            var endpoint = new Endpoint {Id = message.EndpointId};
            
            serviceBus.RegisterEndpoint(endpoint);

        }
    }
}