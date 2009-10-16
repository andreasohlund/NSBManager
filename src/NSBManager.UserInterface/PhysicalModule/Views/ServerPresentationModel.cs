using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using NSBManager.Infrastructure.EventAggregator;
using NSBManager.UserInterface.PhysicalModule.Events;
using NSBManager.UserInterface.PhysicalModule.Model;
using IEventAggregator=Microsoft.Practices.Composite.Events.IEventAggregator;

namespace NSBManager.UserInterface.PhysicalModule.Views
{
    public class ServerPresentationModel : IServerPresentationModel, INotifyPropertyChanged, IListener<PhysicalModelChanged>
    {
        private Server selectedServer;
        public IServerView View { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IPhysicalModel physicalModel;
        private readonly IEventAggregator eventAggregator;
        private readonly ObservableCollection<Server> servers = new ObservableCollection<Server>();

        public ObservableCollection<Server> Servers
        {
            get { return servers; }
        }

        public ICommand SelectedServerCommand { get; set; }

        public ServerPresentationModel(IServerView view, IPhysicalModel physicalModel, IEventAggregator eventAggregator)
        {
            View = view;
            View.Model = this;
            this.physicalModel = physicalModel;
            this.eventAggregator = eventAggregator;

            RefreshServersFromPhysicalModel();
            SelectedServerCommand = new DelegateCommand<Server>(ServerSelected);
        }

        private void ServerSelected(Server server)
        {
            SelectedServer = server;
        }

        private void RefreshServersFromPhysicalModel()
        {
            var groupedServers = physicalModel.Endpoints.GroupBy(x => x.ServerName);

            servers.Clear();
            foreach (var keys in groupedServers)
            {
                servers.Add(new Server { Name = keys.Key });
            }
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

                    if(selectedServer != null)
                    {
                        eventAggregator.GetEvent<ServerSelectedEvent>().Publish(SelectedServer);
                    }
                }
            }
        }

        private void InvokePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) 
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Handle(PhysicalModelChanged message)
        {
            RefreshServersFromPhysicalModel();
        }
    }
}