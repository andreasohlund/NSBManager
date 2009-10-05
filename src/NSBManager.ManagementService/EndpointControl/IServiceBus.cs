using NSBManager.ManagementService.Messages;

namespace NSBManager.ManagementService.EndpointControl
{
    public interface IServiceBus
    {
        void RegisterEndpoint(Endpoint endpoint);
    }
}