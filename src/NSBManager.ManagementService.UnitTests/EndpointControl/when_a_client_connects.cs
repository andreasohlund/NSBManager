using Machine.Specifications;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [Subject("Client communication")]
    public class when_a_client_connects : ContextSpecification<EndpointControlService>
    {
        private Establish context = () => Dependency<IBusTopology>().Stub(t => t.GetSnapshot()).Return(new[] {new Endpoint()});

        private Because of = () => SUT.Handle(new ClientConnectRequest());

        private It should_get_a_topology_snapshot = () =>
            Dependency<IBus>().AssertReply<TopologySnapshotMessage>(r => r.Endpoints.Count == 1);

    }
}