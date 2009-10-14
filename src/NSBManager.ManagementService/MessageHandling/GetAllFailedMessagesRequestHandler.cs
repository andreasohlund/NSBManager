using System.Linq;
using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using FailedMessage=NSBManager.ManagementService.Messages.FailedMessage;

namespace NSBManager.ManagementService.MessageHandling
{
    public class GetAllFailedMessagesRequestHandler : IHandleMessages<GetAllFailedMessagesRequest>
    {
        private readonly IBus bus;
        private readonly IFailedMessagesService failedMessagesService;

        public GetAllFailedMessagesRequestHandler(IBus bus, 
                                                  IFailedMessagesService failedMessagesService)
        {
            this.bus = bus;
            this.failedMessagesService = failedMessagesService;
        }

        public void Handle(GetAllFailedMessagesRequest message)
        {
            bus.Reply(new GetAllFailedMessagesReply
                          {
                              Messages = failedMessagesService.FailedMessages.Select(x=>new FailedMessage{Id = x.Id}).ToList()
                          });
        }
    }
}