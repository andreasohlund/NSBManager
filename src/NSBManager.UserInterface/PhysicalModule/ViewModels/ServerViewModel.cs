using System.Collections.ObjectModel;
using System.Linq;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.UserInterface.PhysicalModule.Events;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class ServerViewModel : BaseViewModel, IListener<PhysicalModelChanged>
    {
        private readonly IPhysicalModel physicalModel;

        private readonly ObservableCollection<Server> servers = new ObservableCollection<Server>();

        public ObservableCollection<Server> Servers
        {
            get { return servers; }
        }

        public ServerViewModel(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;

            RefreshServersFromPhysicalView();
        }

        private void RefreshServersFromPhysicalView()
        {

            var groupedServers = physicalModel.Endpoints.GroupBy(x => x.ServerName);


            servers.Clear();
            foreach (var keys in groupedServers)
            {
                servers.Add(new Server { Name = keys.Key });
            }

        }

        public void Handle(PhysicalModelChanged message)
        {
            RefreshServersFromPhysicalView();
        }
    }
}