using System;
using System.Windows.Input;
using Microsoft.Practices.Composite.Presentation.Commands;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class ShellViewModel : BaseViewModel
    {
        public ICommand NavigateCommand { get; private set; }

        public ShellViewModel()
        {
            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        private void Navigate(string uriString)
        {
            
        }
    }

}