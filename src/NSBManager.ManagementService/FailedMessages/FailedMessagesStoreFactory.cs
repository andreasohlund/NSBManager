using NServiceBus.Unicast.Transport;
using StructureMap;

namespace NSBManager.ManagementService.FailedMessages
{
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