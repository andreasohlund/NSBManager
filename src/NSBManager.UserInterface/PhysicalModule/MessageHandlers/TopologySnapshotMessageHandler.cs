using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.Model;
using NServiceBus;

namespace NSBManager.UserInterface.PhysicalModule.MessageHandlers
{
    public class TopologySnapshotMessageHandler : IHandleMessages<TopologySnapshot>
    {
        private readonly IPhysicalModel physicalModel;
        
        public TopologySnapshotMessageHandler(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;
        }

        public void Handle(TopologySnapshot message)
        {
            physicalModel.UpdateModel(message.Endpoints);

        }
    }
}