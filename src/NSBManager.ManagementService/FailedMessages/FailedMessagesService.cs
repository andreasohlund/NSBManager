using System;
using System.Collections.Generic;
using System.Linq;
using NSBManager.Infrastructure;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.ManagementService.EndpointControl.DomainEvents;
using NSBManager.ManagementService.FailedMessages.DomainEvents;

namespace NSBManager.ManagementService.FailedMessages
{
    public class FailedMessagesService : IFailedMessagesService,
                                            IListener<EndpointStartedEvent>
    {
        private readonly IFailedMessagesSourceFactory failedMessagesSourceFactory;
        private readonly IDomainEvents domainEvents;
        private readonly IList<FailedMessage> failedMessages = new List<FailedMessage>();
        private readonly IDictionary<string, IFailedMessagesSource> sources;

        public FailedMessagesService(IFailedMessagesSourceFactory failedMessagesSourceFactory,
                                     IDomainEvents domainEvents)
        {
            this.failedMessagesSourceFactory = failedMessagesSourceFactory;
            this.domainEvents = domainEvents;

            sources = new Dictionary<string, IFailedMessagesSource>();
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
            MonitorFailedMessagesSource(message.AdressOfFailedMessagesStore);
        }

        public void MonitorFailedMessagesSource(string adressOfFailedMessagesSource)
        {
            if (sources.ContainsKey(adressOfFailedMessagesSource))
                return;

            var source = failedMessagesSourceFactory.CreateFailedMessagesSource(adressOfFailedMessagesSource);

            foreach (var failedMessage in source.GetAllMessages())
            {
                if (!failedMessages.Contains(failedMessage))
                    failedMessages.Add(failedMessage);  
            } 

            source.OnMessageFailed += HandleOnMessageFailed;

            source.StartMonitoring();
            sources.Add(adressOfFailedMessagesSource, source);
        }

        public IEnumerable<FailedMessage> GetAllMessages()
        {
            return failedMessages;
            //foreach (var failedMessagesSource in sources.Values)
            //{
            //    foreach (var message in failedMessagesSource.GetAllMessages())
            //    {
            //        yield return message;
            //    }
            //}
        }
    }
}