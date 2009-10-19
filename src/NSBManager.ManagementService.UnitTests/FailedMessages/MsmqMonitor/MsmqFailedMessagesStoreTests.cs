using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.Threading;
using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.FailedMessageStores;
using NServiceBus.Utils;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace NSBManager.ManagementService.UnitTests.FailedMessages.MsmqMonitor
{
    [TestFixture]
    public class MsmqFailedMessagesStoreTests
    {
        private MsmqFailedMessagesStore messageStore;
        private int numberOfMessagesArrived = 0;
        private const string queueName = "managementservice.unittests@localhost";

        private MessageQueue errorQueue;


        [SetUp]
        public void Setup()
        {

            MsmqUtilities.CreateQueueIfNecessary(queueName);

            var fullPath = MsmqUtilities.GetFullPath(queueName);
            errorQueue = new MessageQueue(fullPath);

            errorQueue.Purge();

            messageStore = new MsmqFailedMessagesStore(queueName);

        }

        [Test]
        public void Parse_NSB_specfic_label_information()
        {
            string label = @"<CorrId></CorrId><WinIdName>SE\ASO</WinIdName><FailedQ>managmentgui@PCASO</FailedQ>";

            AddMessageToQueue(new Message(), label);

            var message = messageStore.GetAllMessages().First();

            message.Origin.ShouldEqual("managmentgui@PCASO");

        }

        [Test]
        public void Can_peek_all_messages_from_queue()
        {
            foreach (var message in messageStore.GetAllMessages())
                Debug.WriteLine(message.Id);
        }

        [Test]
        public void Should_trigger_event_when_a_new_message_arrives()
        {
            messageStore.OnMessageFailed += HandleOnMessageFailed;

            messageStore.StartMonitoring();


            AddMessageToQueue();
            AddMessageToQueue();

            Thread.Sleep(500);
            //sleep
            Assert.AreEqual(numberOfMessagesArrived, 2);



        }

        private void AddMessageToQueue()
        {
            AddMessageToQueue(new Message(),"");
        }
        private void AddMessageToQueue(Message message,string label)
        {
            using (var transaction = new MessageQueueTransaction())
            {
                transaction.Begin();
                errorQueue.Send(message,label, transaction);

                transaction.Commit();
            }

        }

        private void HandleOnMessageFailed(FailedMessage message)
        {
            numberOfMessagesArrived += 1;
        }
    }
}