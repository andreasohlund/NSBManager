using System.Threading;
using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl;
using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.FailedMessageStores;
using NServiceBus.Unicast.Transport.Msmq;
using StructureMap.Configuration.DSL;

namespace NSBManager.ManagementService
{
    public class ManagementServiceRegistry : Registry
    {
        public ManagementServiceRegistry()
        {
            ConfigureDomainEvents();

            ConfigureFailedMessageStores();
            For<IBusTopology>().Singleton().Use<BusTopology>();
        }

        private void ConfigureFailedMessageStores()
        {
            For<IFailedMessagesStoreFactory>().Use<FailedMessagesStoreFactory>();
            For<IFailedMessagesStore>()
                .Use<MsmqFailedMessagesStore>()
                .Named(typeof(MsmqTransport).Name);

        }

        private void ConfigureDomainEvents()
        {
            RegisterInterceptor(new RegisterEventListenersInterceptor());

            For<IDomainEvents>().Singleton().Use<EventAggregator>();
            For<IEventAggregator>().Use(ctx => ctx.GetInstance<IDomainEvents>());


            ForSingletonOf<SynchronizationContext>().Use(() =>
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