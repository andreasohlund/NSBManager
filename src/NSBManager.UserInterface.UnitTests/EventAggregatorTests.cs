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
        private IContainer container;
        private IEventAggregator aggregator;

        [SetUp]
        public void Setup()
        {
            container = new Container(x =>
            {
                x.AddRegistry(new EventRegistry());

                x.ForRequestedType<IListener<TestEvent>>()
                    .TheDefaultIsConcreteType<TestListener>();

            });
            aggregator = container.GetInstance<IEventAggregator>();
            
        }

        [Test]
        public void Can_publish_event()
        {
            container.Configure(x => x.For<IListener<TestEvent>>().Singleton()
                                         .Use<TestListener>());

            var listener = container.GetInstance<TestListener>();
            
            aggregator.Publish(new TestEvent());

            Assert.True(listener.WasCalled);
            Assert.False(listener.Event2WasCalled);
        }

        [Test]
        public void One_shoot_events_for_single_call_instances_spike()
        {
            container.Configure(x => x.For<IOneShootListener<TestEvent>>()
                                         .Use<OneShootListener>());

            var listener = container.GetInstance<OneShootListener>();

            aggregator.Publish(new TestEvent());

            Assert.True(listener.WasCalled);

            listener.WasCalled = false;

            aggregator.Publish(new TestEvent());

            Assert.False(listener.WasCalled);

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


    public class OneShootListener : IOneShootListener<TestEvent>
    {
        public bool WasCalled { get; set; }
        public void Handle(TestEvent message)
        {
            WasCalled = true;
        }

     
    }
    public class TestEvent2
    {
    }

    public class TestEvent
    {
    }
}