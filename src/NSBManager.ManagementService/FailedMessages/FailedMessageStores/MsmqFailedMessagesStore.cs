using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Transactions;
using NServiceBus.Unicast.Transport.Msmq;
using NServiceBus.Utils;

namespace NSBManager.ManagementService.FailedMessages.FailedMessageStores
{
    public class MsmqFailedMessagesStore : IFailedMessagesStore
    {
        private readonly MessageQueue errorQueue;
        private Cursor cursor;
        private readonly string adress;

        public MsmqFailedMessagesStore(string adress)
        {
            this.adress = adress;

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

        public void RetryMessage(FailedMessage message)
        {
            using (var scope = new TransactionScope())
            {
                var m = errorQueue.ReceiveById(message.Id, TimeSpan.FromSeconds(5), MessageQueueTransactionType.Automatic);

                var failedQueue = MsmqTransport.GetFailedQueue(m);

                m.Label = MsmqTransport.GetLabelWithoutFailedQueue(m);

                using (var q = new MessageQueue(failedQueue))
                {
                    q.Send(m, MessageQueueTransactionType.Automatic);
                }

                scope.Complete();
            }
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

        private FailedMessage ConvertToFailedMessage(Message msmqMessage)
        {
            return new FailedMessage
                       {
                           Id = msmqMessage.Id,
                           Origin = GetOriginFromLabel(msmqMessage.Label),
                           AddressOfFailedMessageStore = adress
                       };
        }

        private static string GetOriginFromLabel(string label)
        {
            if(!label.Contains("<FailedQ>"))
                return "";

            int startIndex = label.IndexOf("<FailedQ>") + 9;
            int endIndex = label.IndexOf("<",startIndex);
            return label.Substring(startIndex, endIndex - startIndex);
        }
    }
}