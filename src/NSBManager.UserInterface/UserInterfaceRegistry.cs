using Caliburn.PresentationFramework.ApplicationModel;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.UserInterface.DemoModels;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.ViewModels;
using StructureMap.Configuration.DSL;

namespace NSBManager.UserInterface
{
    public class UserInterfaceRegistry : Registry
    {
        public UserInterfaceRegistry()
        {
            RegisterInterceptor(new RegisterEventListenersInterceptor());
            For<IEventAggregator>().Use<EventAggregator>();
            
            For<IPhysicalModel>().Singleton().Use<PhysicalModel>();
            For<IShell>().Singleton().Use<ShellViewModel>();
            For<IServerViewModel>().Use<ServerViewModel>();
           
            Scan(y =>
            {
                y.TheCallingAssembly();
                y.AddAllTypesOf<IShortcut>();
            });
            

            Profile("demo", x => x.For<IPhysicalModel>().Use<FakePhysicalModel>());
        }
    }
}