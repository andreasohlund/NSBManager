using NSBManager.UserInterface.PhysicalModule.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.Views
{
    public partial class ServerDetailsView
    {
        public ServerDetailsView(ServerDetailsViewModel serverDetailsViewModel)
        {
            InitializeComponent();
            DataContext = serverDetailsViewModel;
        }
    }
}
