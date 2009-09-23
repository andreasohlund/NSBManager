using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.ViewModels;
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

                                            //this line should be replaced with a convention scanner
                                            x.ForConcreteType<EndpointListView>().Configure
                                                .SetterDependency(d => d.DataContext).Is(c => c.OfConcreteType<EndpointListViewModel>());
      
                                        });


            ObjectFactory.Profile = profileToUse;
        }

       
    }
}