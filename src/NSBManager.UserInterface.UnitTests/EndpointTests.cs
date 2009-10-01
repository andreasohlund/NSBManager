using NSBManager.UserInterface.PhysicalModule.Model;
using NUnit.Framework;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class EndpointTests
    {
        [Test]
        public void Can_set_and_receive_endpoint_name()
        {
            var endpoint = new Endpoint {Name = "Endpoint"};

            Assert.That(endpoint.Name.Equals("Endpoint"));
        }
    }
}