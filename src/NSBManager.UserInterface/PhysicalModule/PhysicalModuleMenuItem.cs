using System;
using System.Collections.Generic;
using System.Windows.Input;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.ShellFramework.Results;
using NSBManager.UserInterface.PhysicalModule.ViewModels;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule
{
    public class PhysicalModuleMenuItem : IShortcut
    {
        public IEnumerable<IResult> Execute()
        {
            yield return Show.Child<ServerViewModel>().In<IShell>();
        }

        public string DisplayName
        {
            get { return "Physical Module"; }
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