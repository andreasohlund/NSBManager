using System.Collections.Generic;

namespace NSBManager.ManagementService.FailedMessages
{
    public interface IFailedMessagesService
    {
        IEnumerable<FailedMessage> FailedMessages { get; }
        void MonitorFailedMessagesStores(string adressOfFailedMessagesStore);
    }
}