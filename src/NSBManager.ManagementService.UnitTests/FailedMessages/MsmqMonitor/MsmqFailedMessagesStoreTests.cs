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
    [TestFixture,Ignore("Until we redo the failure management to match the new 2.1 features of NSB")]
    public class MsmqFailedMessagesStoreTests
    {
        private MsmqFailedMessagesStore messageStore;
        private int numberOfMessagesArrived = 0;
        private const string errorQueueAdress = "error.unittests@localhost";
        private const string originQueueAdress = "origin.unittests@localhost";

        private MessageQueue errorQueue;
        private MessageQueue originQueue;

        const string label = @"<CorrId></CorrId><WinIdName>SE\ASO</WinIdName><FailedQ>" + originQueueAdress + "</FailedQ>";


        [SetUp]
        public void Setup()
        {

            MsmqUtilities.CreateQueueIfNecessary(errorQueueAdress);
            MsmqUtilities.CreateQueueIfNecessary(originQueueAdress);

            errorQueue = new MessageQueue(MsmqUtilities.GetFullPath(errorQueueAdress));
            errorQueue.Purge();

            originQueue = new MessageQueue(MsmqUtilities.GetFullPath(originQueueAdress));
            originQueue.Purge();

            messageStore = new MsmqFailedMessagesStore(errorQueueAdress);

        }

        [Test]
        public void Retried_message_should_be_moved_to_origin()
        {
            AddMessageToQueue(new Message(), label);
            var message = messageStore.GetAllMessages().First();

            messageStore.RetryMessage(message);

            errorQueue.GetAllMessages().Count().ShouldEqual(0);
            originQueue.GetAllMessages().Count().ShouldEqual(1);
        }

        [Test]
        public void Parse_NSB_specfic_label_information()
        {
            
            AddMessageToQueue(new Message(), label);

            var message = messageStore.GetAllMessages().First();

            message.Origin.ShouldEqual(originQueueAdress);
            message.AddressOfFailedMessageStore.ShouldEqual(errorQueueAdress);

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