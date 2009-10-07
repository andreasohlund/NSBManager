using System.Collections.Generic;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public interface IPhysicalModel
    {
        IEnumerable<ManagementService.Messages.Endpoint> Endpoints { get; }

        void UpdateModel(IEnumerable<ManagementService.Messages.Endpoint> endpoints);
    }
}