using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.UserInterface.DemoModels;
using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.Views;
using StructureMap;

namespace NSBManager.UserInterface
{
    public abstract class StructureMapBootstrapper : UnityBootstrapper
    {
        public new Container Container { get; private set; }

        protected override void ConfigureContainer()
        {
            this.Container = new Container();

            Container.Configure(x =>
                                        {
                                            x.AddRegistry(new EventRegistry());

                                            x.For<IPhysicalModel>().AsSingletons()
                                                .Use<PhysicalModel>();

                                            x.CreateProfile("demo")
                                                .For<IPhysicalModel>().UseConcreteType<FakePhysicalModel>();

                                            //this line should be replaced with a convention scanner
                                            x.ForConcreteType<EndpointListViewModel>();
                                            x.ForConcreteType<EndpointListView>();
                                            x.ForConcreteType<SelectorRegionAdapter>();
                                            x.ForConcreteType<ItemsControlRegionAdapter>();
                                            x.ForConcreteType<ContentControlRegionAdapter>();
                                            x.ForConcreteType<RegionAdapterMappings>();

                                            x.For<IShellView>().Use<Shell>();
                                            x.For<IRegionManager>().Use<RegionManager>();
                                            x.For<IRegionBehaviorFactory>().Use<RegionBehaviorFactory>();
                                            
                                            // Note ??
                                            x.For<IServiceLocator>().Use<StructureMapServiceLocatorAdapter>();
                                            x.For<IEventAggregator>().Use<EventAggregator>();
                                            x.For<IRegionViewRegistry>().Use<RegionViewRegistry>();

                                            x.For<ShellPresenter>().Use<ShellPresenter>();
                                            x.For<PhysicalModule.PhysicalModule>().Use<PhysicalModule.PhysicalModule>();

                                        });

        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var regionAdapterMappings = Container.TryGetInstance<RegionAdapterMappings>();

            if(regionAdapterMappings != null)
            {
                regionAdapterMappings.RegisterMapping(typeof(Selector), Container.TryGetInstance<SelectorRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ItemsControl), Container.TryGetInstance<ItemsControlRegionAdapter>());
                regionAdapterMappings.RegisterMapping(typeof(ContentControl), Container.TryGetInstance<ContentControlRegionAdapter>());
            }
            return regionAdapterMappings;
        }
    }
}
