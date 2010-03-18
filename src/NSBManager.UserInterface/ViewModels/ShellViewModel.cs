using System;
using System.Collections.Generic;
using System.ComponentModel;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.ApplicationModel;
using Caliburn.PresentationFramework.RoutedMessaging;
using Caliburn.PresentationFramework.Screens;
using Caliburn.ShellFramework.Menus;
using Caliburn.ShellFramework.Results;
using Microsoft.Practices.ServiceLocation;
using NSBManager.UserInterface.PhysicalModule.ViewModels;

namespace NSBManager.UserInterface.ViewModels
{
    public class ShellViewModel : ScreenConductor<IScreen>, IShell
    {
        private readonly IServiceLocator _serviceLocator;
        public MainMenuViewModel MainMenu { get; set; }

        public ShellViewModel(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            MainMenu = _serviceLocator.GetInstance<MainMenuViewModel>();
            
            ActiveScreen = _serviceLocator.GetInstance<StartViewModel>();
        }

        public IEnumerable<IResult> ShowServerView()
        {
            yield return Show.Child<ServerViewModel>().In<IShell>();
        }
      
        public override void OpenScreen(IScreen screen, Action<bool> completed)
        {
            var before = ActiveScreen;

            base.OpenScreen(screen, completed);

            var after = ActiveScreen;
        }
    }
}