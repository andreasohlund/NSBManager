using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [Subject("Bus topology management")]
    public class When_the_bus_topology_is_initialized
    {
        Establish context = () =>
        {
            initialEndpoints = new List<Endpoint>
                                   {
                                       new Endpoint {Id = "1", Status = EndpointStatus.Online,Adress = "test@testserver"}
                                   };

            bus = MockRepository.GenerateStub<IBus>();
            SUT = new BusTopology(bus);
        };

        Because of = () =>SUT.Initialize(initialEndpoints);
        

        It should_assume_that_endpoint_status_is_unknown = () =>
            SUT.GetCurrentEndpoints().First(e => e.Id == "1")
            .Status.ShouldEqual(EndpointStatus.Unknown);

        It should_ping_all_endpoints_for_current_status = () =>
            bus.AssertWasSent<EndpointPingRequest>("test@testserver",m=>true);


        private static BusTopology SUT;
        private static List<Endpoint> initialEndpoints;
        private static IBus bus;
    }
}