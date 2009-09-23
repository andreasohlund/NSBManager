using NSBManager.UserInterface.Events;
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
                    x.ForRequestedType<IEventAggregator>()
                        .TheDefaultIsConcreteType<EventAggregator>()
                        .AsSingletons();

                    x.ForRequestedType<IListener<TestEvent>>()
                        .TheDefaultIsConcreteType<TestListener>();

                    x.RegisterInterceptor(new RegisterEventListenersInterceptor());
                });

            var aggregator = container.GetInstance<IEventAggregator>();
            var listener = container.GetInstance<TestListener>();
            
            aggregator.Publish(new TestEvent());

            Assert.True(listener.WasCalled);
        }
    }

    public class TestListener : IListener<TestEvent>
    {
        public bool WasCalled { get; set; }
        public void Handle(TestEvent message)
        {
            WasCalled = true;
        }
    }

    public class TestEvent
    {
    }
}