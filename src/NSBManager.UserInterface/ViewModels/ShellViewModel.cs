using System;
using System.ComponentModel;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;
using Caliburn.ShellFramework.Menus;

namespace NSBManager.UserInterface.ViewModels
{
    public class ShellViewModel : IShell
    {
        private IObservableCollection<IShortcut> menuItems;

        public IObservableCollection<IShortcut> MenuItems
        {
            get { return menuItems; }
        }

        public ShellViewModel(IShortcut[] menuItems)
        {
            this.menuItems = new BindableCollection<IShortcut>(menuItems);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Initialize()
        {
            
        }

        public bool CanShutdown()
        {
            return true;
        }

        public void Shutdown()
        {
            
        }

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }

        public string DisplayName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public void OpenScreen(IScreen screen, Action<bool> completed)
        {
            //Todo: Display chosen screen
            
        }

        public void ShutdownScreen(IScreen screen, Action<bool> completed)
        {
            
        }

        public IObservableCollection<IScreen> Screens
        {
            get { throw new NotImplementedException(); }
        }

        public void ShutdownActiveScreen(Action<bool> completed)
        {
            throw new NotImplementedException();
        }

        public IScreen ActiveScreen
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}