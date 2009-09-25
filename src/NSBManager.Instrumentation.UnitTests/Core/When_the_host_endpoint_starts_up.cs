using System;
using NSBManager.Instrumentation.Core;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;
using NSBManager.TestHelpers;

namespace NSBManager.Instrumentation.UnitTests.Core
{
    [TestFixture]
    public class When_the_host_endpoint_starts_up
    {
        private IBus bus;
        private TransportInfo transportInfo;
        
        [SetUp]
        public void SetUp()
        {
            bus = MockRepository.GenerateStub<IBus>();
            var transportInspector = MockRepository.GenerateStub<ITransportInspector>();

            transportInfo = new TransportInfo {Adress = "test@localhost"};

            transportInspector.Stub(x => x.GetTransportInfo()).Return(transportInfo);
            var endpointWatcher = new EndpointMonitor(bus, transportInspector);


            endpointWatcher.Start();
        }

        [Test]
        public void A_endpoint_startup_message_should_be_sent_to_the_management_service()
        {
            //transport.adress is guaranteed to be unique across endpoints so we use that a ID
            bus.AssertWasSent<EndpointStartupMessage>(p=>p.EndpointId == transportInfo.Adress);
        }
    }
}