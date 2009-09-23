using StructureMap.Configuration.DSL;

namespace NSBManager.UserInterface.Infrastructure
{
    public class EventRegistry:Registry
    {
        public EventRegistry()
        {
            ForRequestedType<IEventAggregator>()
                      .TheDefaultIsConcreteType<EventAggregator>()
                      .AsSingletons();


            RegisterInterceptor(new RegisterEventListenersInterceptor());
        }
    }
}