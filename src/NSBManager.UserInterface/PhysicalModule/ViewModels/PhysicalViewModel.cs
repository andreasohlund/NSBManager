using System.Collections.Generic;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.PresentationFramework.Screens;
using Caliburn.ShellFramework.Results;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class PhysicalViewModel : Screen
    {
        public IEnumerable<IResult> ShowServerView()
        {
            yield return Show.Child<ServerViewModel>().In<IShell>();
        }
    }
}