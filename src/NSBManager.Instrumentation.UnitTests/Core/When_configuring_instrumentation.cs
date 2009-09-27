using System;
using NSBManager.Instrumentation.Core;
using NSBManager.Instrumentation.Core.Inspectors;
using NServiceBus;
using NUnit.Framework;

namespace NSBManager.Instrumentation.UnitTests.Core
{
    [TestFixture]
    public class When_configuring_instrumentation
    {
        private Configure config;

        [SetUp]
        public void SetUp()
        {
            config = Configure.With(new Type[] {})
                .SpringBuilder()
                .EnableInstrumentation()
                .UnicastBus()
                .MsmqTransport();
        }

        [Test]
        public void The_endpoint_manager_should_be_registered()
        {
            Assert.IsNotNull(config.Builder.Build<IEndpointMonitor>()); 
        }

        [Test]
        public void The_transport_inspector_should_be_registered()
        {
            Assert.IsNotNull(config.Builder.Build<ITransportInspector>());
        }

        [Test]
        public void The_host_inspector_should_be_registered()
        {
            Assert.IsNotNull(config.Builder.Build<IHostInspector>());
        }
    }
} 