using NSBManager.UserInterface.PhysicalModule.Events;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.ViewModels;
using NUnit.Framework;
using Rhino.Mocks;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class EndpointListViewModelTests
    {
        private EndpointListViewModel endpointListViewModel;

        [SetUp]
        public void Setup()
        {
            var physicalModel = MockRepository.GenerateStub<IPhysicalModel>();
            endpointListViewModel = new EndpointListViewModel(physicalModel);
        }


        [Test]
        public void Notify_view_when_physical_model_changes()
        {
            var wasRaised = false;

            endpointListViewModel.PropertyChanged += delegate { wasRaised = true; };
            
            endpointListViewModel.Handle(new PhysicalModelChanged());


            
            Assert.True(wasRaised);
        }

     

    }
}