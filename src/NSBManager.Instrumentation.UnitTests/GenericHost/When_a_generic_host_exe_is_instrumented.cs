using Machine.Specifications;
using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation.UnitTests.GenericHost
{
    [Subject("Host inspectors")]
    public class When_a_generic_host_exe_is_instrumented
    {
        Establish context = () =>
        {
            SUT = new GenericHostInspector();
        };

        Because of = () => hostInformation = SUT.GetHostInformation();

        It should_set_the_host_type_to_generic_host = () =>
           hostInformation.Type.ShouldEqual(HostType.GenericHost);

        private static IHostInspector SUT;
        private static HostInformation hostInformation;
    }
} 