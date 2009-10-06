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

        public ObservableCollection<Server> Servers
        {
            get { return GetServersFromPhysicalView(); }
        }

        public ServerViewModel(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;
        }

        private ObservableCollection<Server> GetServersFromPhysicalView()
        {
            var listOfServers = new ObservableCollection<Server>();

            var servers = physicalModel.Endpoints.GroupBy(x => x.ServerName);

            foreach (var keys in servers)
            {
                listOfServers.Add(new Server{Name = keys.Key});
            }
            return listOfServers;
        }

        public void Handle(PhysicalModelChanged message)
        {
            
        }
    }
}