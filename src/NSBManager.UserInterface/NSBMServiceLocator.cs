using NSBManager.UserInterface.PhysicalModule.ViewModels;
using StructureMap;

namespace NSBManager.UserInterface
{
    public class NSBMServiceLocator
    {
        public ShellViewModel ShellViewModel
        {
            get { return ObjectFactory.GetInstance<ShellViewModel>(); }
        }

        public ServerViewModel ServerViewModel
        {
            get { return ObjectFactory.GetInstance<ServerViewModel>(); }
        }
    }
}