using System;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.UserInterface.Infrastructure;
using NUnit.Framework;
using StructureMap;

namespace NSBManager.UserInterface.UnitTests
{
    [TestFixture]
    public class EventAggregatorTests
    {
        [Test]
        public void Can_publish_event()
        {
            var container = new Container(x =>
                {
                    x.AddRegistry(new EventRegistry());
                    
                    x.ForRequestedType<IListener<TestEvent>>()
                        .TheDefaultIsConcreteType<TestListener>();

                });

            var aggregator = container.GetInstance<IEventAggregator>();
            var listener = container.GetInstance<TestListener>();
            
            aggregator.Publish(new TestEvent());

            Assert.True(listener.WasCalled);
            Assert.False(listener.Event2WasCalled);
        }
    }

    public class TestListener : IListener<TestEvent>, IListener<TestEvent2>
    {
        public bool WasCalled { get; set; }
        public bool Event2WasCalled { get; set; }
        public void Handle(TestEvent message)
        {
            WasCalled = true;
        }

        public void Handle(TestEvent2 message)
        {
            Event2WasCalled = true;
        }
    }

    public class TestEvent2
    {
    }

    public class TestEvent
    {
    }
}