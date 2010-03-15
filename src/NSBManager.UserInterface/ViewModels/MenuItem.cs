using System;
using System.Collections.Generic;
using System.Windows.Input;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.ShellFramework.Menus;
using Caliburn.ShellFramework.Results;

namespace NSBManager.UserInterface.ViewModels
{
    public class MenuItem : IShortcut
    {
        public IEnumerable<IResult> Execute()
        {
            yield return Show.Child<ServerViewModel>().In<IShell>();
        }

        public string DisplayName
        {
            get { return "Test Menu Item"; }
        }

        public ModifierKeys Modifers
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public Key Key
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public bool CanExecute
        {
            get { return true; }
        }
    }
}