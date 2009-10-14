using System;
using System.Collections.Generic;

namespace NSBManager.ManagementService.FailedMessages
{
    public interface IFailedMessagesSource
    {
        event Action<FailedMessage> OnMessageFailed;
        IEnumerable<FailedMessage> GetAllMessages();
        void StartMonitoring();
    }
}