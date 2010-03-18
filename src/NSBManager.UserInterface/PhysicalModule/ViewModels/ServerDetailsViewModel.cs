using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.PresentationFramework.Screens;
using Caliburn.ShellFramework.Results;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class ServerDetailsViewModel : Screen
    {
        private readonly IPhysicalModel physicalModel;

        public string ServerName { get; set; }
        public Server Server { get; set; }
        public IObservableCollection<GuiEndpoint> Endpoints { get; set; }

        public ServerDetailsViewModel(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;
        }

        public IEnumerable<IResult> Back()
        {
            yield return Show.Child<ServerViewModel>().In<IShell>();
        }
    }
}