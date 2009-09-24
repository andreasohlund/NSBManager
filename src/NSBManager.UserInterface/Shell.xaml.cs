using System;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.Views;
using StructureMap;

namespace NSBManager.UserInterface
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : IShellView
    {
        public Shell()
        {
            InitializeComponent();
        }

        public void ShowView()
        {
            Show();

            //Note: Just testing... Do it the right way later!
            contentControl.Content = ObjectFactory.GetInstance<EndpointListView>();
        }
    }
}
