using System.Threading;
using System.Windows.Threading;
using NSBManager.Infrastructure.EventAggregator;
using StructureMap.Configuration.DSL;

namespace NSBManager.UserInterface.Infrastructure
{
    public class EventRegistry:Registry
    {
        public EventRegistry()
        {
            RegisterInterceptor(new RegisterEventListenersInterceptor());

            For<IEventAggregator>().Singleton().Use<EventAggregator>();

            ForSingletonOf<SynchronizationContext>().TheDefault.Is.ConstructedBy(() =>
            {
                if (SynchronizationContext.Current == null)
                {
                    SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());
                }

                return SynchronizationContext.Current;
            });
        }
    }
}