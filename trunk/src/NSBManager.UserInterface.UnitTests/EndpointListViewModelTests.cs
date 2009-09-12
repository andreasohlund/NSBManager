using NSBManager.UserInterface.ViewModels;
using NUnit.Framework;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class EndpointListViewModelTests
    {
        private EndpointListViewModel endpointListViewModel;

        [SetUp]
        public void Setup()
        {
            endpointListViewModel = new EndpointListViewModel();
        }


        [Test]
        public void Can_add_endpoints()
        {
            endpointListViewModel.Endpoints.Add(new Endpoint());

            Assert.That(endpointListViewModel.Endpoints.Count > 0);
        }

        [Test]
        public void Can_set_and_receive_endpointList_name()
        {
            endpointListViewModel.EndpointListName = "TestName";

            Assert.That(endpointListViewModel.EndpointListName.Equals("TestName"));
        }
    }
}