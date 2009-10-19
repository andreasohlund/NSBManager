using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Presentation.Regions.Behaviors;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.ServiceLocation;
using NSBManager.UserInterface.DemoModels;
using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.Views;
using StructureMap;

namespace NSBManager.UserInterface
{
    public abstract class StructureMapBootstrapper
    {
        public void Run()
        {
            Run(string.Empty);
        }

        public void Run(string profileToUse)
        {
            ConfigureContainer(profileToUse);
            ConfigureRegionAdapterMappings();
            ConfigureDefaultRegionBehaviors();

            DependencyObject shell = CreateShell();
            if(shell != null)
            {
                RegionManager.SetRegionManager(shell, ObjectFactory.GetInstance<IRegionManager>());
                RegionManager.UpdateRegions();
            }

            InitializeModules();
        }

        protected void ConfigureContainer(string profileToUse)
        {
            //IModuleCatalog catalog = GetModuleCatalog();

            //if(catalog != null)
            //{
            //    ObjectFactory.Configure(x => x.Register(catalog));
            //}
            
            ObjectFactory.Configure(x =>
                                        {
                                            x.AddRegistry(new EventRegistry());
                                            x.AddRegistry(new UserInterfaceRegistry());
                                            x.AddRegistry(new PrismRegistry());
                                        });

            ObjectFactory.Profile = profileToUse;

            ServiceLocator.SetLocatorProvider(ObjectFactory.GetInstance<IServiceLocator>);
        }
        //protected virtual IModuleCatalog GetModuleCatalog()
        //{
        //    return null;
        //}
        protected RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var regionAdapterMappings = ObjectFactory.TryGetInstance<RegionAdapterMappings>();

            if(regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Selector), ObjectFactory.GetInstance<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ItemsControl), ObjectFactory.GetInstance<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ContentControl), ObjectFactory.GetInstance<ContentControlRegionAdapter>());
            }
            return regionAdapterMappings;
        }

        protected virtual IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var defaultRegionBehaviorTypesDictionary = ObjectFactory.GetInstance<IRegionBehaviorFactory>();

            if (defaultRegionBehaviorTypesDictionary != null)
            {
                defaultRegionBehaviorTypesDictionary.AddIfMissing(AutoPopulateRegionBehavior.BehaviorKey,
                    typeof(AutoPopulateRegionBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(BindRegionContextToDependencyObjectBehavior.BehaviorKey,
                    typeof(BindRegionContextToDependencyObjectBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionActiveAwareBehavior.BehaviorKey,
                    typeof(RegionActiveAwareBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(SyncRegionContextWithHostBehavior.BehaviorKey,
                    typeof(SyncRegionContextWithHostBehavior));

                defaultRegionBehaviorTypesDictionary.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey,
                    typeof(RegionManagerRegistrationBehavior));

            }
            return defaultRegionBehaviorTypesDictionary;

        }

        protected virtual void InitializeModules()
        {
            var manager = ObjectFactory.GetInstance<IModuleManager>();

            manager.Run();
        }

        protected abstract DependencyObject CreateShell();
    }
}
