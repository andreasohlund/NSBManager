using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Messaging;
using NSBManager.ManagementService.FailedMessages;
using NServiceBus.Utils;
using NUnit.Framework;
using System.Linq;

namespace NSBManager.ManagementService.UnitTests.FailedMessages.MsmqMonitor
{
    [TestFixture]
    public class MsmqFailedMessagesSourceTests
    {
        [Test, Explicit("Manual test")]
        public void Can_peek_all_messages_from_queue()
        {
            var messageSource = new MsmqFailedMessagesSource("managementservice.subscriptions@localhost");

            foreach (var message in messageSource.GetAllMessages())
                Debug.WriteLine(message.Id);
        }
    }

    public class MsmqFailedMessagesSource : IFailedMessagesSource
    {
        private readonly string errorQueueAdress;

        public MsmqFailedMessagesSource(string errorQueueAdress)
        {
            this.errorQueueAdress = errorQueueAdress;
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