using System;
using System.Collections.ObjectModel;
using System.Linq;
using NSBManager.UserInterface.Events;
using NSBManager.UserInterface.Models;

namespace NSBManager.UserInterface.ViewModels
{
    public class EndpointListViewModel : BaseViewModel, 
                                         IListener<PhysicalModelChanged>
    {
        
        //Note: Is this property nessesary to raise the event?
        private string endpointListName;
        public string EndpointListName
        {
            get { return endpointListName; }
            set
            {
                if(endpointListName != value)
                {
                    endpointListName = value;
                    this.RaisePropertyChanged(d => d.EndpointListName);
                }
            }
        }

        private ObservableCollection<Endpoint> endpoints;
        public ObservableCollection<Endpoint> Endpoints
        {
            get { return endpoints; }
            set 
            {
                endpoints = value;
                this.RaisePropertyChanged(d => d.Endpoints);
            }
        }

        public EndpointListViewModel()
        {

            endpoints = new ObservableCollection<Endpoint>();

            endpoints.Add(new Endpoint{Name = "test"});

        }

        public void Handle(PhysicalModelChanged message)
        {

            endpoints.Clear();
            foreach (var endpoint in message.Endpoints)
            {
                endpoints.Add(endpoint);
            }

        }

      
    }
}