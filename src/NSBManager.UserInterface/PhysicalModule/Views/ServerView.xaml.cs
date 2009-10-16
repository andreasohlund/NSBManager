using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NSBManager.UserInterface.PhysicalModule.Views
{
    /// <summary>
    /// Interaction logic for ServerView.xaml
    /// </summary>
    public partial class ServerView : IServerView
    {
        public ServerView()
        {
            InitializeComponent();
        }

        public IServerPresentationModel Model
        {
            get { return DataContext as IServerPresentationModel; }
            set { DataContext = value; }
        }
    }
}
