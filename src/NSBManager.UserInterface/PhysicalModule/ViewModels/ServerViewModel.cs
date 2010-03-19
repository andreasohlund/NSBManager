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

        public IObservableCollection<Server> Servers
        {
            get { return servers; }
        }

        public ServerViewModel(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;

            RefreshServersFromPhysicalModel();
        }

        private void RefreshServersFromPhysicalModel()
        {
            var groupedServers = physicalModel.Endpoints.GroupBy(x => x.ServerName);

            servers.Clear();

            foreach (var keys in groupedServers)
            {
                servers.Add(new Server{ Name = keys.Key });
            }
        }

        public IEnumerable<IResult> ShowServerDetails(Server server)
        {
            //Todo: Add logic to show ServerDetailsView
            yield return Show.Child<ServerDetailsViewModel>().In<IShell>();
        }
    }

    public interface IServerViewModel : IScreen
    {
    }
}