using NSBManager.Instrumentation.Core.Messages;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.MessageHandling;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [TestFixture]
    public class When_a_endpointstartup_message_is_received
    {
        private string endpointId;
        private IBusTopology busTopology;

        private string serverName = "server";

        [SetUp]
        public void SetUp()
        {
            busTopology = MockRepository.GenerateStub<IBusTopology>();

            IHandleMessages<EndpointStartupMessage> messageHandler = new EndpointStartupMessageHandler(busTopology);

            endpointId = "test@localhost";


            var endpointStartupMessage = new EndpointStartupMessage
                                             {
                                                 EndpointId = endpointId,
                                                 Server = serverName
                                             };


            messageHandler.Handle(endpointStartupMessage);

        }

        [Test]
        public void The_servicebus_should_be_notified()
        {
            busTopology.AssertWasCalled(b => b.RegisterEndpoint(Arg<Endpoint>.Matches(
                                                                    e => e.Id == endpointId &&
                                                                         e.ServerName == serverName)));
        }

       



    }
}