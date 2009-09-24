using System;
using NSBManager.Instrumentation.Core;
using NServiceBus;
using NServiceBus.Host;
using NUnit.Framework;

namespace NSBManager.Instrumentation.UnitTests.GenericHost
{
    [TestFixture]
    public class When_the_host_initializes
    {
        [Test]
        public void The_endpoint_monitor_should_be_configured()
        {
            IWantCustomInitialization configuration = new Configuration();

            Configure.With()
                .SpringBuilder()
                .UnicastBus()
                .MsmqTransport();

            configuration.Init();

            Assert.AreNotEqual(null,  Configure.Instance.Builder.Build<IEndpointMonitor>());
        }
    }
}