using System.Collections.Generic;
using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.FailedMessages.DomainEvents;
using NServiceBus.Host;

namespace NSBManager.ManagementService.FailedMessages
{
    public class FailedMessagesService :    IFailedMessagesService,
                                            IWantToRunAtStartup,
                                            IListener<EndpointStartedEvent>
    {
        private readonly IFailedMessagesStoreFactory failedMessagesStoreFactory;
        private readonly IDomainEvents domainEvents;
        private readonly IList<FailedMessage> failedMessages = new List<FailedMessage>();
        private readonly IDictionary<string, IFailedMessagesStore> failedMessagesStores;

        public FailedMessagesService(IFailedMessagesStoreFactory failedMessagesStoreFactory,
                                     IDomainEvents domainEvents)
        {
            this.failedMessagesStoreFactory = failedMessagesStoreFactory;
            this.domainEvents = domainEvents;

            failedMessagesStores = new Dictionary<string, IFailedMessagesStore>();
        }

        public IEnumerable<FailedMessage> FailedMessages
        {
            get
            {
                return failedMessages;
            }
        }

        private void HandleOnMessageFailed(FailedMessage failedMessage)
        {
            lock (failedMessages)
                if (!failedMessages.Contains(failedMessage))
                {
                    failedMessages.Add(failedMessage);

                    domainEvents.Publish(new FailedMessageDetectedEvent
                    {
                        FailedMessage = failedMessage
                    });
                }
        }


        public void Handle(EndpointStartedEvent message)
        {
            MonitorFailedMessagesStores(message.AdressOfFailedMessagesStore);
        }

        public void MonitorFailedMessagesStores(string adressOfFailedMessagesStore)
        {
            if (failedMessagesStores.ContainsKey(adressOfFailedMessagesStore))
                return;

            var store = failedMessagesStoreFactory.CreateFailedMessagesStore(adressOfFailedMessagesStore);

            foreach (var failedMessage in store.GetAllMessages())
            {
                if (!failedMessages.Contains(failedMessage))
                    failedMessages.Add(failedMessage);  
            } 

            store.OnMessageFailed += HandleOnMessageFailed;

            store.StartMonitoring();
            failedMessagesStores.Add(adressOfFailedMessagesStore, store);
        }

        public IEnumerable<FailedMessage> GetAllMessages()
        {
            return failedMessages;
        }

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}