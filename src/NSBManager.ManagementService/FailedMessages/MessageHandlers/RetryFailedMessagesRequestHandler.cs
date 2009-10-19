using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.ManagementService.FailedMessages.MessageHandlers
{
    public class RetryFailedMessagesRequestHandler : IHandleMessages<RetryFailedMessagesRequest>
    {
        private readonly IFailedMessagesService failedMessagesService;

        public RetryFailedMessagesRequestHandler(IFailedMessagesService failedMessagesService)
        {
            this.failedMessagesService = failedMessagesService;
        }

        public void Handle(RetryFailedMessagesRequest request)
        {
            failedMessagesService.RetryMessage(request.MessageId);
        }
    }
}