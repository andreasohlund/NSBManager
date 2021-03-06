using System;
using System.Collections.Generic;
using System.Threading;

namespace NSBManager.Infrastructure.EventAggregator
{
    public class EventAggregator : IDomainEvents
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
            var potentialListeners = new List<object>(listeners);

            foreach (var potentialListener in potentialListeners)
            {
                if (potentialListener is IListener<T>)
                {
                    var listener = potentialListener as IListener<T>;

                    context.Send(state => listener.Handle(message), null);

                    if (listener is IOneShootListener<T>)
                    {
                        lock (locker)
                        {
                            listeners.Remove(listener);
                        }

                    }
                }

            }
        }

        public void Publish<T>() where T : new()
        {
            Publish(new T());
        }

        public void RegisterListener<T>(IListener<T> listener)
        {
            lock (locker)
            {
                if (!listeners.Contains(listener))
                    listeners.Add(listener);
            }

        }

    }
}