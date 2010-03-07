using System;
using Machine.Specifications;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [Subject("Endpoint manager startup")]
    public class When_known_endpoints_is_loaded
    {
        Establish context = () =>
        {
            bus = MockRepository.GenerateStub<IBus>();
            SUT = new EndpointManager();
        };

        Because of = () =>
            SUT.Initialize();

        It should_mark_the_customer_as_preferred = () =>
            bus.AssertWasNotCalled(b => b.Send());

        private static EndpointManager SUT;
        private static IBus bus;
    }

    public class EndpointManager
    {
        public void Initialize()
        {


        }
    }
}