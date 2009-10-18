namespace NSBManager.ManagementService.FailedMessages
{
    public interface IFailedMessagesStoreFactory
    {
        IFailedMessagesStore CreateFailedMessagesStore(string address);
    }
}