using System;
using NSBManager.Instrumentation.Core.Inspectors.Transport;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus.Unicast.Transport.Msmq;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace NSBManager.Instrumentation.UnitTests.Core.Inspectors
{
    [TestFixture]
    public class MsmqTransportInspectorTests
    {
        private TransportInfo transportInfo;
        private MsmqTransport transport;
        [SetUp]
        public void Setup()
        {

            transport = new MsmqTransport
            {
                InputQueue = "unittests",
                ErrorQueue ="error"
            };

            var inspector = new MsmqTransportInspector(transport);

            transportInfo = inspector.GetTransportInformation();
        }

        [Test]
        public void Should_get_adress_from_transport()
        {

            transportInfo.ShouldBeInstanceOfType(typeof(MsmqTransportInfo));

            transportInfo.Adress.ShouldEqual(transport.InputQueue + "@" + Environment.MachineName);
        }

        [Test]
        public void Should_get_adress_of_failed_messages_store()
        {
            transportInfo.AdressOfFailedMessageStore.ShouldEqual(transport.ErrorQueue+"@" + Environment.MachineName);
        }
    }
} 