using NSBManager.UserInterface.PhysicalModule.ViewModels;

namespace NSBManager.UserInterface.Views
{
    /// <summary>
    /// Interaction logic for EndpointListView.xaml
    /// </summary>
    public partial class EndpointListView
    {

        public EndpointListView(EndpointListViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
