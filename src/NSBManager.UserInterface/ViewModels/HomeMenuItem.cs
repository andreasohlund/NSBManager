using System;
using System.Collections.Generic;
using System.Windows.Input;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.ShellFramework.Results;
using NSBManager.UserInterface.PhysicalModule.ViewModels;

namespace NSBManager.UserInterface.ViewModels
{
    public class HomeMenuItem : IShortcut
    {
        public IEnumerable<IResult> Execute()
        {
            //Note: This triggers the ShellViewModel's base OpenScreen(...)
            yield return Show.Child<StartViewModel>().In<IShell>();
        }

        public string DisplayName
        {
            get { return "Home"; }
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