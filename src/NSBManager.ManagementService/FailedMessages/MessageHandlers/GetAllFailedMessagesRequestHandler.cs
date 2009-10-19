using System.Linq;
using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.ManagementService.FailedMessages.MessageHandlers
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
                              Messages = failedMessagesService.FailedMessages.Select(x => new Messages.FailedMessage
                                                                                              {
                                                                                                  Id = x.Id,
                                                                                                  Origin = x.Origin
                                                                                              }).ToList()
                          });
        }
    }
}