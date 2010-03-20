using NSBManager.Instrumentation.Core;
using NServiceBus;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.Instrumentation.UnitTests.GenericHost
{
    [TestFixture]
    public class When_the_host_starts
    {
        [Test]
        public void The_endpoint_monitor_should_be_started()
        {
            var endpointMonitor = MockRepository.GenerateStub<IEndpointMonitor>();

            IWantToRunAtStartup hostMonitor = new HostMonitor(endpointMonitor);

            hostMonitor.Run();

            endpointMonitor.AssertWasCalled(x => x.Start());
        }
    }
} 