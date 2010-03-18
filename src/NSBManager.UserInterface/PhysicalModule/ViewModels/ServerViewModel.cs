using System.Collections.Generic;
using System.Linq;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.PresentationFramework.Screens;
using Caliburn.ShellFramework.Results;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class ServerViewModel : Screen, IServerViewModel
    {
        private readonly IPhysicalModel physicalModel;

        private readonly IObservableCollection<Server> servers = new BindableCollection<Server>();

        public IObservableCollection<Server> Servers { get; private set; }

        public ServerViewModel(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;

            Servers = new BindableCollection<Server>(this.physicalModel.Servers());
        }


        public IEnumerable<IResult> ShowServerDetails(Server server)
        {
            //Note: This doesn't seem right. We need some sort of Endpoints collection on the Server object
            yield return Show.Child<ServerDetailsViewModel>()
                                .In<IShell>()
                                .Configured(x =>
                                                {
                                                    x.ServerName = server.Name; 
                                                    x.Endpoints =new BindableCollection<GuiEndpoint>(physicalModel.EndpointsOnServer(server.Name));
                                                });
        }
    }
}