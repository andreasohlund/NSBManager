using System;
using System.Linq;
using Machine.Specifications;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [Subject(typeof(BusTopology), "Topology transitions")]
    public class when_a_endpoint_is_started : ContextSpecification<BusTopology>
    {
        Establish context = () =>
        {
            startedEndpoint = new Endpoint
            {
                Id = "1@localhost"
            };
        };

        private Because of = () => SUT.EndpointStarted(startedEndpoint);

        private It should_be_added_to_the_topology = () =>
            SUT.GetSnapshot().Count().ShouldEqual(1);


        It should_trigger_a_endpoint_started_event = () =>
            Dependency<IBus>().
                AssertWasCalled(x => x.Publish<IEndpointStartedEvent>(Arg<Action<IEndpointStartedEvent>>.Is.Anything));


        private It should_have_status_set_to_running = () =>
            SUT.GetSnapshot().First()
                .Status.ShouldEqual(EndpointStatus.Running);


        private static Endpoint startedEndpoint;
    }
}