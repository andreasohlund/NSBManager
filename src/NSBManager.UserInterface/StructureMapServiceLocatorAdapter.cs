using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace NSBManager.UserInterface
{
    public class StructureMapServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private readonly Container container;

        public StructureMapServiceLocatorAdapter(Container container)
        {
            this.container = container;
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return container.TryGetInstance(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType) as IEnumerable<object>;
        }
    }
}