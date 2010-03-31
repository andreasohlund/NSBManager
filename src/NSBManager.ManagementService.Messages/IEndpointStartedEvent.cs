namespace NSBManager.ManagementService.Messages
{
    public interface IEndpointStartedEvent:ITopologyChangedEvent
    {
        Endpoint StartedEndpoint { get; set; }
    }
}