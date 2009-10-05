using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using NSBManager.UserInterface.Views;
using StructureMap;

namespace NSBManager.UserInterface.PhysicalModule
{
    public class PhysicalModule : IModule
    {
        private readonly IRegionManager regionManager;

        public PhysicalModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            IRegion mainRegion = regionManager.Regions["MainRegion"];
            mainRegion.Add(ObjectFactory.GetInstance<EndpointListView>());
        }
    }
}
