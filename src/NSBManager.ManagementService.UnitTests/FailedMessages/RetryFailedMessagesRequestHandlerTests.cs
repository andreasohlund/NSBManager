using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.MessageHandlers;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.FailedMessages
{
    [TestFixture]
    public class RetryFailedMessagesRequestHandlerTests
    {
        [Test]
        public void HandleRetryRequest()
        {
            var failedMessagesService = MockRepository.GenerateStub<IFailedMessagesService>();


            IHandleMessages<RetryFailedMessagesRequest> handler =
                new RetryFailedMessagesRequestHandler(failedMessagesService);

            handler.Handle(new RetryFailedMessagesRequest { MessageId = "1" });

            failedMessagesService.AssertWasCalled(x => x.RetryMessage("1"));

        }
    }
}