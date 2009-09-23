using NSBManager.UserInterface.Events;
using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.Models;
using NUnit.Framework;
using System.Collections.Generic;
using Rhino.Mocks;
using System.Linq;

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


            eventAggregator.AssertWasCalled(x => x.Publish(Arg<PhysicalModelChanged>.Matches(arg => arg.Endpoints.Count() == 1)));
        }
    }
} 