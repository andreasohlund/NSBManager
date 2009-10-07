using System.Linq;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.Model;
using NServiceBus;

using Endpoint=NSBManager.UserInterface.PhysicalModule.Model.Endpoint;

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
            physicalModel.UpdateModel(message.Endpoints.Select(x => new Endpoint
                                                                        {
                                                                            Name = x.Id,
                                                                            ServerName = x.Id
                                                                        }));

        }
    }
}