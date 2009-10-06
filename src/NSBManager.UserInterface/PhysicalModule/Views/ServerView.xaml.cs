using System;
using System.Windows.Controls;
using NSBManager.UserInterface.PhysicalModule.Model;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using StructureMap;

namespace NSBManager.UserInterface.PhysicalModule.Views
{
    public partial class ServerView
    {
        public ServerView(ServerViewModel serverViewModel)
        {
            InitializeComponent();
            DataContext = serverViewModel;
        }
    }
}
