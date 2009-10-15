using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using NServiceBus.Utils;

namespace NSBManager.ManagementService.FailedMessages.FailedMessageSources
{
    public class MsmqFailedMessagesSource : IFailedMessagesSource
    {
        private readonly string errorQueueAdress;

        public MsmqFailedMessagesSource(string adress)
        {
            errorQueueAdress = adress;
        }

        public event Action<FailedMessage> OnMessageFailed;
        
        public void StartMonitoring()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FailedMessage> GetAllMessages()
        {
            var fullPath = MsmqUtilities.GetFullPath(errorQueueAdress);

            var errorQueue = new MessageQueue(fullPath)
                                 {
                                     MessageReadPropertyFilter =
                                         {
                                             Id = true,
                                             Priority = true,
                                             SentTime = true,
                                             MessageType = true,
                                             Label = true,


                                         }
                                 };

            return errorQueue.GetAllMessages().Select(m => new FailedMessage
                                                               {
                                                                   Id = m.Id
                                                               });
        }
    }
}