using System.Collections.Generic;
using NSBManager.UserInterface.Models;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public interface IPhysicalModel
    {
        IEnumerable<Endpoint> Endpoints { get; }

        void UpdateModel(IEnumerable<Endpoint> endpoints);
    }
}