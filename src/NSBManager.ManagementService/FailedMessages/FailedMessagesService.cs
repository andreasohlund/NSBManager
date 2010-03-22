using System;
using System.Collections.Generic;
using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.FailedMessages.DomainEvents;
using NSBManager.ManagementService.Messages;
using NServiceBus;
using System.Linq;

namespace NSBManager.ManagementService.FailedMessages
{
    public class FailedMessagesService :    IFailedMessagesService,
                                            IWantToRunAtStartup,
                                            IListener<EndpointStartedEvent>
    {
        private readonly IFailedMessagesStoreFactory failedMessagesStoreFactory;
        private readonly IDomainEvents domainEvents;
        private readonly IList<FailedMessage> failedMessages = new List<FailedMessage>();
        private readonly IDictionary<string, IFailedMessagesStore> failedMessageStores;

        public FailedMessagesService(IFailedMessagesStoreFactory failedMessagesStoreFactory,
                                     IDomainEvents domainEvents)
        {
            this.failedMessagesStoreFactory = failedMessagesStoreFactory;
            this.domainEvents = domainEvents;

            failedMessageStores = new Dictionary<string, IFailedMessagesStore>();
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
            MonitorFailedMessagesStores(message.Endpoint.AdressOfFailedMessageStore);
        }

        public void MonitorFailedMessagesStores(string adressOfFailedMessagesStore)
        {
            if (failedMessageStores.ContainsKey(adressOfFailedMessagesStore))
                return;

            var store = failedMessagesStoreFactory.CreateFailedMessagesStore(adressOfFailedMessagesStore);

            foreach (var failedMessage in store.GetAllMessages())
            {
                if (!failedMessages.Contains(failedMessage))
                    failedMessages.Add(failedMessage);  
            } 

            store.OnMessageFailed += HandleOnMessageFailed;

            store.StartMonitoring();
            failedMessageStores.Add(adressOfFailedMessagesStore, store);
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

        public void RetryMessage(string messageId)
        {
            var message = failedMessages.Where(x => x.Id == messageId).FirstOrDefault();

            if (message == null)
                throw new Exception("FailedMessage with id: " + messageId + " not found");

            var store = failedMessageStores[message.AddressOfFailedMessageStore];

            store.RetryMessage(message);
        }
    }
}