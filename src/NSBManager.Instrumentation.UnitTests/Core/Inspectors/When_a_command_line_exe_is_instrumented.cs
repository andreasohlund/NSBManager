using System;
using Machine.Specifications;
using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Inspectors.Host;
using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation.UnitTests.Core.Inspectors
{
    [Subject("Host inspectors")]
    public class When_a_command_line_exe_is_instrumented
    {
        Establish context = () =>
        {
            
            SUT = new ExeHostInspector();
        };

        Because of = () => hostInformation = SUT.GetHostInformation();


        It should_return_the_current_working_directory_of_the_process = () =>
            hostInformation.WorkingDir.ShouldEqual(Environment.CurrentDirectory);

        It should_set_the_host_type_to_commandline_exe = () =>
           hostInformation.Type.ShouldEqual(HostType.CommandLineExe);
       
        private static IHostInspector SUT;
        private static HostInformation hostInformation; 
    }
}