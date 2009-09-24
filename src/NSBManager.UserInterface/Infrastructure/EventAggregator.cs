using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using NSBManager.UserInterface.Events;
using StructureMap;

namespace NSBManager.UserInterface.Infrastructure
{
    public class EventAggregator : IEventAggregator
    {
        private readonly IList<object> listeners;

        public EventAggregator()
        {
            listeners = new List<object>();
        }

        public void Publish<T>(T message)
        {
            foreach (var listener in listeners)
            {
                if (listener is IListener<T>)
                {

                    Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, (ThreadStart)(() => (
                                                          listener as IListener<T>).Handle(message))
                                                          );

                }

            }
        }

        public void RegisterListener<T>(IListener<T> listener)
        {
            listeners.Add(listener);
        }

    }
}