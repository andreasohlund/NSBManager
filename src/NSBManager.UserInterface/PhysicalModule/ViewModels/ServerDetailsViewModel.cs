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
        private readonly Server server;
        private readonly IPhysicalModel physicalModel;

        public string ServerName { get { return server.Name; } }
        
        public IObservableCollection<GuiEndpoint> Endpoints { get; set; }

        public ServerDetailsViewModel(Server server, IPhysicalModel physicalModel)
        {
            this.server = server;
            this.physicalModel = physicalModel;

            Endpoints = new BindableCollection<GuiEndpoint>(GetEndpointsFromPhysicalModel());
        }

        private IEnumerable<GuiEndpoint> GetEndpointsFromPhysicalModel()
        {
            return physicalModel.EndpointsOnServer(ServerName);
        }

        public IEnumerable<IResult> Back()
        {
            yield return Show.Child<ServerViewModel>().In<IShell>();
        }
    }
}