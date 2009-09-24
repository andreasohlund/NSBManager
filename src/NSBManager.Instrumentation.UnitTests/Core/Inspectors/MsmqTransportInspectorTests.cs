using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus.Unicast.Transport.Msmq;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace NSBManager.Instrumentation.UnitTests.Core.Inspectors
{
    [TestFixture]
    public class MsmqTransportInspectorTests
    {
        [Test]
        public void Can_read_adress_from_transport()
        {
            var transport = new MsmqTransport
                                {
                                    InputQueue = "unittests@localhost"
                                };

            var inspector = new MsmqTransportInspector(transport);

            var info = inspector.GetTransportInfo();

            info.Type.ShouldEqual(TransportTypes.Msmq);

            info.Adress.ShouldEqual("unittests@localhost");
        }
    }
} 