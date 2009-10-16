using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Logging;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
//using NSBManager.Infrastructure.EventAggregator;
using StructureMap.Configuration.DSL;

namespace NSBManager.UserInterface.Infrastructure
{
    public class PrismRegistry : Registry
    {
        public PrismRegistry()
        {
            ForConcreteType<SelectorRegionAdapter>();
            ForConcreteType<ItemsControlRegionAdapter>();
            ForConcreteType<ContentControlRegionAdapter>();
            ForConcreteType<DelayedRegionCreationBehavior>();
            ForSingletonOf<RegionAdapterMappings>();

            ForSingletonOf<IServiceLocator>().Use<StructureMapServiceLocatorAdapter>();
            ForSingletonOf<IEventAggregator>().Use<EventAggregator>();
            ForSingletonOf<IRegionViewRegistry>().Use<RegionViewRegistry>();
            ForSingletonOf<IRegionManager>().Use<RegionManager>();
            ForSingletonOf<IRegionBehaviorFactory>().Use<RegionBehaviorFactory>();
            ForSingletonOf<IModuleManager>().Use<ModuleManager>();
            ForSingletonOf<IModuleInitializer>().Use<ModuleInitializer>();
            ForSingletonOf<ILoggerFacade>().Use<EmptyLogger>();
        }
    }
}