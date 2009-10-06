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
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.Views;
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
            IModuleCatalog catalog = GetModuleCatalog();

            if(catalog != null)
            {
                ObjectFactory.Configure(x => x.Register(catalog));
            }
            
            ObjectFactory.Configure(x =>
                                        {
                                            x.AddRegistry(new EventRegistry());

                                            x.For<IPhysicalModel>().AsSingletons()
                                                .Use<PhysicalModel>();

                                            x.CreateProfile("demo")
                                                .For<IPhysicalModel>().UseConcreteType<FakePhysicalModel>();

                                            //this line should be replaced with a convention scanner
                                            
                                            x.ForConcreteType<ShellPresenter>();
                                            x.For<IShellView>().Use<Shell>();

                                            // Modules
                                            x.ForConcreteType<PhysicalModule.PhysicalModule>();
                                            x.ForConcreteType<EndpointListViewModel>();
                                            x.ForConcreteType<EndpointListView>();

                                            // For Prism
                                            x.AddRegistry(new PrismRegistry());

                                        });

            ObjectFactory.Profile = profileToUse;

            ServiceLocator.SetLocatorProvider(ObjectFactory.GetInstance<IServiceLocator>);
        }
        protected virtual IModuleCatalog GetModuleCatalog()
        {
            return null;
        }
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
            IModuleManager manager;

            try
            {
                manager = ObjectFactory.GetInstance<IModuleManager>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }

            manager.Run();
        }

        protected abstract DependencyObject CreateShell();
    }
}
