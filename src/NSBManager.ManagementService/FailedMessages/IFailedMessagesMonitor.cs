using System;

namespace NSBManager.ManagementService.FailedMessages
{
    public interface IFailedMessagesMonitor
    {
        event Action<FailedMessage> OnMessageFailed;
        void StartMonitoring(string adress);
    }
}