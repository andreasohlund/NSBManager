using System.Collections.ObjectModel;
using System.Linq;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.UserInterface.PhysicalModule.Events;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class EndpointListViewModel : BaseViewModel,
                                         IListener<PhysicalModelChanged>
    {
        private readonly IPhysicalModel physicalModel;


        public ObservableCollection<Endpoint> Endpoints
        {
            get
            {
                return new ObservableCollection<Endpoint>( physicalModel.Endpoints
                    .Select(x => new Endpoint{Name = x.Id,ServerName = x.Id}));
            }
        }

        public EndpointListViewModel(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;
        }

        public void Handle(PhysicalModelChanged message)
        {
            //dialog.Show("Topology changed, refresh?")
            this.RaisePropertyChanged(d => d.Endpoints);
        }

      
    }
}