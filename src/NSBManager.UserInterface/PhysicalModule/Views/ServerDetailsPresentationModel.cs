using System.ComponentModel;
using NSBManager.UserInterface.PhysicalModule.Events;
using NSBManager.UserInterface.PhysicalModule.Model;
using IEventAggregator=Microsoft.Practices.Composite.Events.IEventAggregator;

namespace NSBManager.UserInterface.PhysicalModule.Views
{
    public class ServerDetailsPresentationModel : IServerDetailsPresentationModel, INotifyPropertyChanged
    {
        private readonly IPhysicalModel physicalModel;
        private readonly IEventAggregator eventAggregator;
        private Server selectedServer;
        public IServerDetailsView View { get; set; }
        

        public ServerDetailsPresentationModel(IServerDetailsView view, IPhysicalModel physicalModel, IEventAggregator eventAggregator)
        {
            this.physicalModel = physicalModel;
            this.eventAggregator = eventAggregator;
            View = view;
            View.Model = this;

            this.eventAggregator.GetEvent<ServerSelectedEvent>().Subscribe(ServerSelected);
        }

        private void ServerSelected(Server newSelectedServer)
        {
            SelectedServer = newSelectedServer;
        }

        public Server SelectedServer
        {
            get
            {
                return selectedServer;    
            } 
            set
            {
                if(selectedServer != value)
                {
                    selectedServer = value;
                    InvokePropertyChanged("SelectedServer");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}