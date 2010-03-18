using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.Screens;
using Caliburn.ShellFramework.Menus;

namespace NSBManager.UserInterface.ViewModels
{
    public class MainMenuViewModel : Screen
    {
        private IObservableCollection<IShortcut> menuItems;

        public IObservableCollection<IShortcut> MenuItems
        {
            get { return menuItems; }
        }

        public MainMenuViewModel(IShortcut[] menuItems)
        {
            this.menuItems = new BindableCollection<IShortcut>(menuItems);
        }
    }
}