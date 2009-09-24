using System;

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
        }
    }
}
