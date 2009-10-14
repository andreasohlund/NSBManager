using System;
using System.Collections.Generic;
using NServiceBus;

namespace NSBManager.ManagementService.Messages
{
    public class GetAllFailedMessagesReply:IMessage
    {
        public List<FailedMessage> Messages { get; set; }
    }

    public class FailedMessage
    {
        public string Id { get; set; }
    }
}