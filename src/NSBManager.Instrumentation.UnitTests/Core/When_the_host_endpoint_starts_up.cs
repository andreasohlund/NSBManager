using System;
using NSBManager.Instrumentation.Core;
using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.Instrumentation.UnitTests.Core
{
    [TestFixture]
    public class When_the_host_endpoint_starts_up
    {
        private IBus bus;
        private TransportInfo transportInfo;
        private HostInformation hostInformation;

        [SetUp]
        public void SetUp()
        {
            bus = MockRepository.GenerateStub<IBus>();
            var transportInspector = MockRepository.GenerateStub<ITransportInspector>();

            transportInfo = new TransportInfo { Adress = "test@localhost" };

            transportInspector.Stub(x => x.GetTransportInformation()).Return(transportInfo);

            var hostInspector = MockRepository.GenerateStub<IHostInspector>();

            hostInformation = new HostInformation();
            hostInspector.Stub(x => x.GetHostInformation()).Return(hostInformation);

            var endpointMonotor = new EndpointMonitor(bus,
                transportInspector,
                hostInspector);

            endpointMonotor.Start();
        }

        [Test]
        public void A_endpoint_startup_message_should_be_sent_to_the_management_service()
        {
            //transport.adress is guaranteed to be unique across endpoints so we use that a ID
            bus.AssertWasSent<EndpointStartupMessage>(p => p.EndpointId == transportInfo.Adress &&
                p.Server == Environment.MachineName &&
                p.Host == hostInformation);
        }


    }
}