using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.FailedMessageStores;
using NServiceBus.Unicast.Transport;
using NServiceBus.Unicast.Transport.Msmq;
using NUnit.Framework;
using StructureMap;
using NBehave.Spec.NUnit;

namespace NSBManager.ManagementService.UnitTests.FailedMessages
{
    [TestFixture]
    public class FailedMessagesStoreFactoryTests
    {
        [Test]
        public void Resolve_msmq_store()
        {
            
            IContainer container = new Container(x => x.AddRegistry(new ManagementServiceRegistry()));

            IFailedMessagesStoreFactory factory = new FailedMessagesStoreFactory(container,new MsmqTransport());

            var store = factory.CreateFailedMessagesStore("error@server");

            store.ShouldNotBeNull();

            store.ShouldBeInstanceOfType(typeof (MsmqFailedMessagesStore));
        }
    }

    public class FailedMessagesStoreFactory : IFailedMessagesStoreFactory
    {
        private readonly IContainer container;
        private readonly ITransport transport;

        public FailedMessagesStoreFactory(IContainer container,ITransport transport)
        {
            this.container = container;
            this.transport = transport;
        }

        public IFailedMessagesStore CreateFailedMessagesStore(string address)
        {
            return container.With("adress").EqualTo(address)
                .GetInstance<IFailedMessagesStore>(transport.GetType().Name);
        }
    }
} 