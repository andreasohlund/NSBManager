using System;
using StructureMap;
using StructureMap.Interceptors;

namespace NSBManager.UserInterface.Infrastructure
{
    public class RegisterEventListenersInterceptor : TypeInterceptor
    {
        public object Process(object target, IContext context)
        {
            context.GetInstance<IEventAggregator>().AddListener(target);

            return target;
        }

        public bool MatchesType(Type type)
        {
            foreach (var i in type.GetInterfaces())
            {
                if(i.Name.StartsWith("IListener"))
                    return true;
            }
            return false;
        }
    }
}