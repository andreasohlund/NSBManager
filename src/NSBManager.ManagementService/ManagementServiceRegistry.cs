using System.Threading;
using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl;
using StructureMap.Configuration.DSL;

namespace NSBManager.ManagementService
{
    public class ManagementServiceRegistry : Registry
    {
        public ManagementServiceRegistry()
        {
            ConfigureDomainEvents();
            For<IBusTopology>().AsSingletons().Use<BusTopology>();
        }

        private void ConfigureDomainEvents()
        {
            RegisterInterceptor(new RegisterEventListenersInterceptor());

            For<IDomainEvents>().AsSingletons().Use<EventAggregator>();
            For<IEventAggregator>().TheDefault.Is.ConstructedBy(ctx => ctx.GetInstance<IDomainEvents>());


            ForSingletonOf<SynchronizationContext>().TheDefault.Is.ConstructedBy(() =>
                                                                                     {
                                                                                         if (SynchronizationContext.Current == null)
                                                                                         {
                                                                                             SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
                                                                                         }

                                                                                         return SynchronizationContext.Current;
                                                                                     });
        }
    }
}