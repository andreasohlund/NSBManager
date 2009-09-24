using System.Collections.Generic;

namespace NSBManager.UserInterface.Models
{
    public interface IPhysicalModel
    {
        void UpdateModel(IEnumerable<Endpoint> endpoints);
    }
}