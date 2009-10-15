using System.Diagnostics;
using NSBManager.ManagementService.FailedMessages.FailedMessageSources;
using NUnit.Framework;

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
} 