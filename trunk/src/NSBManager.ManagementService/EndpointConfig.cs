using NServiceBus.Host;

namespace NSBManager.ManagementService
{
    public class EndpointConfig:IConfigureThisEndpoint,
                                AsA_Publisher
    {
    }
}