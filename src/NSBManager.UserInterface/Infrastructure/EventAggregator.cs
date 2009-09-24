using System.Collections.Generic;
using System.Threading;

namespace NSBManager.UserInterface.Infrastructure
{
    public class EventAggregator : IEventAggregator
    {
        private readonly SynchronizationContext context;
        private readonly IList<object> listeners;

        private readonly object locker = new object();

        public EventAggregator(SynchronizationContext context)
        {
            listeners = new List<object>();
            this.context = context;
        }

        public void Publish<T>(T message)
        {
            foreach (var potentialListener in listeners)
            {
                if (potentialListener is IListener<T>)
                {
                    var listener = potentialListener as IListener<T>;

                    context.Send(state => listener.Handle(message), null);
                }

            }
        }

        public void RegisterListener<T>(IListener<T> listener)
        {
            lock(locker)
                listeners.Add(listener);
        }

    }
}