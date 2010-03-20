using NUnit.Framework;
using NBehave.Spec.NUnit;

namespace NSBManager.Instrumentation.UnitTests.GenericHost
{
    [TestFixture]
    public class GenericHostInspectorTests
    {
        [Test]
        public void Get_information_should_return_GenericHostInfo()
        {
            var inspector = new GenericHostInspector();

            var info = inspector.GetHostInformation();

            info.ShouldBeInstanceOfType(typeof(GenericHostInformation));
        }
    }
} 