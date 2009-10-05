
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Presentation.Regions;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Composite.UnityExtensions;
using NSBManager.UserInterface.DemoModels;
using System.Windows;
using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.Views;
using StructureMap;

namespace NSBManager.UserInterface
{
    public class Bootstrapper : UnityBootstrapper
    {
        public void BootstrapStructureMap(string profileToUse)
        {
            ObjectFactory.Configure(x =>
                                        {
                                            x.AddRegistry(new EventRegistry());

                                            x.For<IPhysicalModel>().AsSingletons()
                                                .Use<PhysicalModel>();

                                            x.CreateProfile("demo")
                                                .For<IPhysicalModel>().UseConcreteType<FakePhysicalModel>();


                                            //this line should be replaced with a convention scanner
                                            x.ForConcreteType<EndpointListViewModel>();
                                            x.ForConcreteType<EndpointListView>();
                                            x.For<IShellView>().Use<Shell>();
                                            //x.For<IRegionManager>().Use<RegionManager>();
                                            x.For<ShellPresenter>().Use<ShellPresenter>();
                                            x.For<PhysicalModule.PhysicalModule>().Use<PhysicalModule.PhysicalModule>();

                                        });


            ObjectFactory.Profile = profileToUse;
        }

        protected override void InitializeModules()
        {
            //IModule physicalModule = ObjectFactory.GetInstance<PhysicalModule.PhysicalModule>();
            IModule physicalModule = Container.Resolve<PhysicalModule.PhysicalModule>();
            physicalModule.Initialize();
        }

        protected override DependencyObject CreateShell()
        {
            //var presenter = ObjectFactory.GetInstance<ShellPresenter>();
            //IShellView view = presenter.View;

            //view.ShowView();

            var shell = new Shell();
            shell.Show();
            return shell;
        }

        
    }
}
