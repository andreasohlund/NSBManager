using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.MessageHandlers;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using NSBManager.TestHelpers;
using System.Collections.Generic;

namespace NSBManager.ManagementService.UnitTests.FailedMessages
{
    [TestFixture]
    public class When_all_failed_messages_is_requested
    {
        //[Test]
        //public void A_snapshot_of_the_currently_failed_messages_should_be_returned()
        //{
        //    var bus = MockRepository.GenerateStub<IBus>();
        //    var failedMessagesService = MockRepository.GenerateStub<IFailedMessagesService>();

        //    failedMessagesService.Stub(x => x.FailedMessages).Return(new List<ManagementService.FailedMessages.FailedMessage>
        //                                                                 {
        //                                                                     new ManagementService.FailedMessages.FailedMessage{Origin ="test@server"}, 
        //                                                                     new ManagementService.FailedMessages.FailedMessage()
        //                                                                 });

        //    IHandleMessages<GetAllFailedMessagesRequest> handler = new GetAllFailedMessagesRequestHandler(bus, failedMessagesService);

        //    handler.Handle(new GetAllFailedMessagesRequest());

        //    bus.AssertReply<GetAllFailedMessagesReply>(x=>x.Messages.Count == 2);

        //    bus.AssertReply<GetAllFailedMessagesReply>(x => x.Messages[0].Origin == "test@server");
        //}
    }
} 