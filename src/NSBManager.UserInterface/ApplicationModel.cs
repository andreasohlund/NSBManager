using Caliburn.PresentationFramework.Screens;
using Microsoft.Practices.ServiceLocation;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface
{
    public class ApplicationModel : Screen
    {
        private readonly IServiceLocator serviceLocator;
        public ServerViewModel Servers { get; set; }

        public ApplicationModel(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
            Servers = this.serviceLocator.GetInstance<ServerViewModel>();
        }
    }
}