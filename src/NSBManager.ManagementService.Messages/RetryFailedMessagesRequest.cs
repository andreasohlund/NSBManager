using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    public class RetryFailedMessagesRequest : IMessage
    {
        public string MessageId { get; set; }
    }
}