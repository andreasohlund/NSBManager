using NSBManager.UserInterface.ViewModels;
using NUnit.Framework;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class When_a_ViewModel_property_is_changed
    {
        [Test]
        public void PropertyChanged_event_should_be_raised()
        {
            bool wasRaised = false;

            var endpointListViewModel = new EndpointListViewModel();
            endpointListViewModel.PropertyChanged += delegate { wasRaised = true; };
            endpointListViewModel.EndpointListName = "NewBusListName";

            Assert.True(wasRaised);
        }
    }
}