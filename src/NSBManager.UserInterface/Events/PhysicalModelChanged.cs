using System.Collections.Generic;
using NSBManager.UserInterface.Models;

namespace NSBManager.UserInterface.Events
{
    public class PhysicalModelChanged
    {
        public IEnumerable<Endpoint> Endpoints { get; set; }
    }
}