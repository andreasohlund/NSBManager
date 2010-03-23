using System.Linq;
using Machine.Specifications;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [Subject("Topology transitions")]
    public class when_a_endpoint_is_started : context_specification<BusTopology>
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


        private It should_result_in_a_notification_to_the_clients = () =>
            Dependency<IBus>().AssertWasPublished<EndpointStartedEvent>(e => e.Endpoint.Id == startedEndpoint.Id);

        private It should_have_status_set_to_running = () =>
            SUT.GetSnapshot().First()
                .Status.ShouldEqual(EndpointStatus.Running);


        private static Endpoint startedEndpoint;
    }
}