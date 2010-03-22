using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.Model;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.EndpointControl.MessageHandlers;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    [Subject("Endpoint control")]
    public class When_the_service_is_started

    {
        private Establish context = () =>
                                        {
                                            busTopology = MockRepository.GenerateStub<IBusTopology>();
                                            SUT = new EndpointControlService(null,busTopology);
                                        };

        private Because of = () => 
            SUT.Run();

        private It should_initialize_the_bus_topology = () => 
            busTopology.AssertWasCalled(t => t.Initialize(Arg<IEnumerable<Endpoint>>.Is.Anything));

        private static IWantToRunAtStartup SUT;
        private static IBusTopology busTopology;
    }
}