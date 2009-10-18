using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using NServiceBus.Utils;

namespace NSBManager.ManagementService.FailedMessages.FailedMessageStores
{
    public class MsmqFailedMessagesStore : IFailedMessagesStore
    {
        private readonly MessageQueue errorQueue;
        private Cursor cursor;
        public MsmqFailedMessagesStore(string adress)
        {
            var fullPath = MsmqUtilities.GetFullPath(adress);

            errorQueue = new MessageQueue(fullPath)
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
        }

        public event Action<FailedMessage> OnMessageFailed;

        public void StartMonitoring()
        {
            cursor = errorQueue.CreateCursor();

            errorQueue.BeginPeek(MessageQueue.InfiniteTimeout, cursor, PeekAction.Current, null, OnPeekCompleted);
        }


        private void OnPeekCompleted(IAsyncResult asyncResult)
        {
            var message = errorQueue.EndPeek(asyncResult);
            
            OnMessageFailed(ConvertToFailedMessage(message));

            errorQueue.BeginPeek(MessageQueue.InfiniteTimeout, cursor, PeekAction.Next, null, OnPeekCompleted);
        }

        public IEnumerable<FailedMessage> GetAllMessages()
        {

            return errorQueue.GetAllMessages().Select(m => ConvertToFailedMessage(m));
        }

        private static FailedMessage ConvertToFailedMessage(Message msmqMessage)
        {
            return new FailedMessage
                       {
                           Id = msmqMessage.Id
                       };
        }
    }
}