using NSBManager.Infrastructure.EventAggregator;
using NSBManager.UserInterface.PhysicalModule.Events;
using NSBManager.UserInterface.PhysicalModule.Model;
using NUnit.Framework;
using System.Collections.Generic;
using Rhino.Mocks;

namespace NSBManager.UserInterface.UnitTests.Models
{
    [TestFixture]
    public class PhysicalModelTests
    {
        [Test]
        public void UpdateModel()
        {
            var eventAggregator = MockRepository.GenerateStub<IEventAggregator>();

            var model = new PhysicalModel(eventAggregator);

            model.UpdateModel(new List<Endpoint>{new Endpoint()});


            eventAggregator.AssertWasCalled(x => x.Publish<PhysicalModelChanged>());
        }
    }
} 