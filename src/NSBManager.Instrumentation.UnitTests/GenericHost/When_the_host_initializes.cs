using System;
using NSBManager.Instrumentation.Core;
using NSBManager.Instrumentation.Core.Inspectors;
using NServiceBus;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace NSBManager.Instrumentation.UnitTests.GenericHost
{
    [TestFixture]
    public class When_the_host_initializes
    {
        private IWantCustomInitialization configuration;

        [SetUp]
        public void Setup()
        {
            configuration = new Configuration();

            Configure.With(new Type[] { })
                .DefaultBuilder()
                .UnicastBus()
                .MsmqTransport();

            configuration.Init();


        }

        [Test]
        public void The_endpoint_monitor_should_be_configured()
        {
            Configure.Instance.Builder.Build<IEndpointMonitor>().ShouldNotBeNull();
        }

        [Test]
        public void The_nservicebus_generic_host_inspector_should_be_configured()
        {
            Configure.Instance.Builder.Build<IHostInspector>()
                .ShouldBeInstanceOfType(typeof(GenericHostInspector));
        }
    }
}