using NSBManager.UserInterface.PhysicalModule.ViewModels;
using StructureMap;

namespace NSBManager.UserInterface
{
    public class ServiceLocatorAdapter
    {
        //Note: This is temporary until I find a way to get the xaml to hook into StructureMapServiceLocatorAdapter

        private readonly IContainer container;

        public ServiceLocatorAdapter()
        {
            container = ObjectFactory.GetInstance<IContainer>();
        }

        public ServerViewModel ServerViewModel
        {
            get { return container.GetInstance<ServerViewModel>(); }
        }
    }
}