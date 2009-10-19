using Microsoft.Practices.Composite.Modularity;
using NSBManager.UserInterface.DemoModels;
using NSBManager.UserInterface.PhysicalModule.Model;
using StructureMap.Configuration.DSL;
using StructureMap;

namespace NSBManager.UserInterface
{
    public class UserInterfaceRegistry : Registry
    {
        public UserInterfaceRegistry()
        {
            For<IPhysicalModel>().AsSingletons()
                                                .Use<PhysicalModel>();

            CreateProfile("demo")
                .For<IPhysicalModel>().UseConcreteType<FakePhysicalModel>();

            ForConcreteType<ShellPresenter>();
            For<IShellView>().Use<Shell>();

            // Modules
            For<IModule>().AsSingletons()
                .AddInstances(i => i.OfConcreteType<PhysicalModule.PhysicalModule>());

            For<IModuleCatalog>().TheDefault.Is.ConstructedBy(ctx =>
                                                                  {
                                                                      var catalog = new ModuleCatalog();
                                                                      foreach(var module in ObjectFactory.GetAllInstances<IModule>())
                                                                        catalog.AddModule(module.GetType());

                                                                      return catalog;
                                                                  });
            
        }
    }
}