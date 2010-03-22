using Machine.Specifications;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [Subject("Client communication")]
    public class when_a_client_connects
    {
        private Establish context = () =>
                                        {
                                            bus = MockRepository.GenerateStub<IBus>();
                                            busTopology = MockRepository.GenerateStub<IBusTopology>();
                                            busTopology.Stub(t => t.GetSnapshot()).Return(new[] {new Endpoint()});

                                            SUT = new EndpointControlService(bus,busTopology);
                                        };

        private Because of = () => SUT.Handle(new ClientConnectRequest());

        private It should_get_a_topology_snapshot = () =>
            bus.AssertReply<TopologySnapshotMessage>(r => r.Endpoints.Count == 1);

        private static IHandleMessages<ClientConnectRequest> SUT;
        private static IBus bus;
        private static IBusTopology busTopology;
    }
}