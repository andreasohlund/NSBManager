using System;
using StructureMap;
using StructureMap.Interceptors;
using StructureMap.TypeRules;

namespace NSBManager.Infrastructure.EventAggregator
{
    public class RegisterEventListenersInterceptor : TypeInterceptor
    {
        public object Process(object target, IContext context)
        {
            // Assuming that "target" is an implementation of IListener<T>,
            var eventType = target.GetType().FindInterfaceThatCloses(typeof (IListener<>)).GetGenericArguments()[0];
            var type = typeof (Registration<>).MakeGenericType(eventType);
            var registration = (Registration) Activator.CreateInstance(type);
            
            registration.RegisterListener(context, target);
 
            // we didn't change the target object, so just return it
            return target;
        }

        public bool MatchesType(Type type)
        {
            return type.ImplementsInterfaceTemplate(typeof(IListener<>));
        }


        // The inner type and interface is just a little trick to
        // grease the generic wheels
        public interface Registration
        {
            void RegisterListener(IContext context, object listener);
        }

        public class Registration<T> : Registration
        {
            public void RegisterListener(IContext context, object listener)
            {
                var aggregator = context.GetInstance<IEventAggregator>();
                aggregator.RegisterListener((IListener<T>)listener);
            }
        }
    }
}