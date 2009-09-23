using NSBManager.UserInterface.Events;
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
                                            x.ForConcreteType<EndpointListView>();

                                            x.ForRequestedType<IEventAggregator>()
                                                .TheDefaultIsConcreteType<EventAggregator>()
                                                .AsSingletons();

                                            x.ForRequestedType<BaseViewModel>( )
                                                .Use<EndpointListViewModel>().WithName("EndpointListViewModel");
                                                
                                            //x.CreateProfile("UIMockup").For<EndpointListViewModel>()
                                              //  .UseConcreteType<FakeEndpointListViewModel>();

                                                
                                        });


            ObjectFactory.Profile = profileToUse;
        }

       
    }

    public class FakeEndpointListViewModel
    {
        
    }
}