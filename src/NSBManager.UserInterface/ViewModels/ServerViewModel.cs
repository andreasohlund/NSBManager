using System.Linq;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.Screens;
using NSBManager.UserInterface.PhysicalModule.Model;

namespace NSBManager.UserInterface.ViewModels
{
    public class ServerViewModel : Screen
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

        public void ShowServerDetails(Server server)
        {
            //Todo: Add logic to show ServerDetailsView
        }
    }
}