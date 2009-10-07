using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.Model;
using NServiceBus;

namespace NSBManager.UserInterface.PhysicalModule.MessageHandlers
{
    public class BusTopologyChangedEventHandler : IHandleMessages<BusTopologyChangedEvent>
    {
        private readonly IPhysicalModel physicalModel;
        
        public BusTopologyChangedEventHandler(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;
        }

        public void Handle(BusTopologyChangedEvent message)
        {
            physicalModel.UpdateModel(message.Endpoints);

        }
    }
}