using System;
using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Inspectors.Host;
using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace NSBManager.Instrumentation.UnitTests.Core.Inspectors
{
    [TestFixture]
    public class ExeHostInspectorTests
    {
        [Test]
        public void Return_working_dir_of_the_host()
        {
            IHostInspector inspector = new ExeHostInspector();

            var hostInfo = inspector.GetHostInformation();


            hostInfo.WorkingDir.ShouldEqual(Environment.CurrentDirectory); 
        }
    }
}