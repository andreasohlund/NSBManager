namespace NSBManager.ManagementService.FailedMessages
{
    public interface IFailedMessagesSourceFactory
    {
        IFailedMessagesSource CreateFailedMessagesSource(string address);
    }
}