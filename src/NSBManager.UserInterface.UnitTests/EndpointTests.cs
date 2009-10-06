using NSBManager.UserInterface.PhysicalModule.Model;
using NUnit.Framework;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class EndpointTests
    {
        private Endpoint endpoint;

        [SetUp]
        public void SetUp()
        {
            endpoint = new Endpoint { Name = "Endpoint", ServerName = "ServerName" };
        }

        [Test]
        public void Can_set_and_receive_endpoint_name()
        {
            Assert.That(endpoint.Name.Equals("Endpoint"));
        }

        [Test]
        public void Can_set_and_receive_server_name()
        {
            Assert.That(endpoint.ServerName.Equals("ServerName"));
        }
    }
}