using System;

namespace NSBManager.UserInterface.PhysicalModule.Views
{
    public interface IServerPresentationModel
    {
        IServerView View { get; }
    }
}