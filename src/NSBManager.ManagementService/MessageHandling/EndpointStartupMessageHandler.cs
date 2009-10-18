using NSBManager.Instrumentation.Core.Messages;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.ManagementService.MessageHandling
{
    public class EndpointStartupMessageHandler:IHandleMessages<EndpointStartupMessage>
    {
        private readonly IBusTopology busTopology;
     

        public EndpointStartupMessageHandler(IBusTopology busTopology)
        {
            this.busTopology = busTopology;
        }

        public void Handle(EndpointStartupMessage message)
        {
            var endpoint = new Endpoint
                               {
                                   Id = message.EndpointId,
                                   ServerName = message.Server,
                                   AdressOfFailedMessageStore = message.Transport.AdressOfFailedMessageStore
                               };
            
            busTopology.RegisterEndpoint(endpoint);

        }
    }
}