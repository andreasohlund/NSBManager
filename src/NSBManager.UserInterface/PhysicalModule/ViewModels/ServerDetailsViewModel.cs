using System.Collections.Generic;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.PresentationFramework.Screens;
using Caliburn.ShellFramework.Results;
using NSBManager.UserInterface.ViewModels;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class ServerDetailsViewModel : Screen
    {
        public IEnumerable<IResult> Back()
        {
            yield return Show.Child<ServerViewModel>().In<IShell>();
        }
    }
}