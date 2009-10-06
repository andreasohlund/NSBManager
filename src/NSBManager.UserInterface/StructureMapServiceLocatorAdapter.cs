using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace NSBManager.UserInterface
{
    public class StructureMapServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly IContainer container;

        public StructureMapServiceLocatorAdapter()
        {
            this.container = ObjectFactory.GetInstance<IContainer>();
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if(key == null)
            {
                return container.GetInstance(serviceType);
            }
            
            return container.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType) as IEnumerable<object>;
        }
    }
}