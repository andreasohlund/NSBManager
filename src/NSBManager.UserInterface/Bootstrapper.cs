using NSBManager.UserInterface.DemoModels;
using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.Views;
using StructureMap;

namespace NSBManager.UserInterface
{
    public class Bootstrapper
    {
        public void BootstrapStructureMap(string profileToUse)
        {
            ObjectFactory.Configure(x=>
                                        {
                                            x.AddRegistry(new EventRegistry());

                                            x.For<IPhysicalModel>().AsSingletons()
                                                .Use<PhysicalModel>();
                                            x.CreateProfile("demo")
                                                .For<IPhysicalModel>().UseConcreteType<FakePhysicalModel>();

                                            //this line should be replaced with a convention scanner
                                            x.ForConcreteType<EndpointListViewModel>();
                                            x.ForConcreteType<EndpointListView>();
                                            
                                        });


            ObjectFactory.Profile = profileToUse;
        }

       
    }
}