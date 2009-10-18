using System;
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
            For<IBusTopology>().AsSingletons().Use<BusTopology>();
        }

        private void ConfigureFailedMessageStores()
        {
            For<IFailedMessagesStoreFactory>().Use<FailedMessagesStoreFactory>();
            For<IFailedMessagesStore>()
                .Use<MsmqFailedMessagesStore>()
                .WithName(typeof(MsmqTransport).Name);

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