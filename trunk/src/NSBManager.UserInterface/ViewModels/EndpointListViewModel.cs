using System.Collections.ObjectModel;

namespace NSBManager.UserInterface.ViewModels
{
    public class EndpointListViewModel : BaseViewModel
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

            //Todo: Remove this later on
            GenerateTemporaryEndpoints();
        }

        private void GenerateTemporaryEndpoints()
        {
            for(int i = 0; i < 5; i++)
            {
                endpoints.Add(new Endpoint{ Name = string.Format("Endpoint nr. {0}", i) });
            }
        }
    }
}