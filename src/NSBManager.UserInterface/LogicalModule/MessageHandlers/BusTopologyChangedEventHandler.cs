using System.Linq;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.Models;
using NServiceBus;
using Endpoint=NSBManager.UserInterface.Models.Endpoint;

namespace NSBManager.UserInterface.LogicalModule.MessageHandlers
{
    public class BusTopologyChangedEventHandler : IHandleMessages<BusTopologyChangedEvent>
    {
        private readonly LogicalModel logicalModel;

        public BusTopologyChangedEventHandler(LogicalModel logicalModel)
        {
            this.logicalModel = logicalModel;
        }

        public void Handle(BusTopologyChangedEvent message)
        {
            //TODO
        }
    }

    public class LogicalModel
    {
    }
}