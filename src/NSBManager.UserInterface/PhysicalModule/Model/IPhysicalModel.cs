using System.Collections.Generic;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public interface IPhysicalModel
    {
        IEnumerable<Endpoint> Endpoints { get; }

        void UpdateModel(IEnumerable<Endpoint> endpoints);
    }
}