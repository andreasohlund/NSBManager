using System;
using System.Collections.Generic;
using System.Linq;
using NSBManager.UserInterface.Events;
using StructureMap;

namespace NSBManager.UserInterface.Infrastructure
{
    public class EventAggregator:IEventAggregator

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
                if(listener is IListener<T>)
                 (listener as IListener<T>).Handle(message);
            }
        }

        public void AddListener(object listener)
        {
            listeners.Add(listener);
        }
    }
}