using System.Diagnostics;
using System.Messaging;
using System.Threading;
using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.FailedMessageSources;
using NServiceBus.Utils;
using NUnit.Framework;

namespace NSBManager.ManagementService.UnitTests.FailedMessages.MsmqMonitor
{
    [TestFixture]
    public class MsmqFailedMessagesSourceTests
    {
        private MsmqFailedMessagesSource messageSource;
        private int numberOfMessagesArrived = 0;
        private const string queueName = "managementservice.unittests@localhost";

        private MessageQueue errorQueue;


        [SetUp]
        public void Setup()
        {
            messageSource = new MsmqFailedMessagesSource(queueName);
            var fullPath = MsmqUtilities.GetFullPath(queueName);

            errorQueue = new MessageQueue(fullPath);

        }

        [Test, Explicit("Manual test")]
        public void Can_peek_all_messages_from_queue()
        {

            foreach (var message in messageSource.GetAllMessages())
                Debug.WriteLine(message.Id);
        }

        [Test, Explicit("Manual test")]
        public void Should_trigger_event_when_a_new_message_arrives()
        {
            messageSource.OnMessageFailed += HandleOnMessageFailed;

            messageSource.StartMonitoring();

            var currentNumberOfMessages = errorQueue.GetAllMessages().Length;

            using (var transaction = new MessageQueueTransaction())
            {
                transaction.Begin();
                errorQueue.Send(new Message() ,transaction);
                errorQueue.Send(new Message(), transaction);

                transaction.Commit();
            }
            
            Thread.Sleep(1000);
            //sleep
            Assert.AreEqual(numberOfMessagesArrived, currentNumberOfMessages + 2);



        }

        private void HandleOnMessageFailed(FailedMessage message)
        {
            numberOfMessagesArrived+=1;
        }
    }
}