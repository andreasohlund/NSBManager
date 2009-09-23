using NSBManager.UserInterface.Infrastructure;
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
                                            x.ForConcreteType<EndpointListView>();
      
                                        });


            ObjectFactory.Profile = profileToUse;
        }

       
    }

    public class FakeEndpointListViewModel
    {
        
    }
}