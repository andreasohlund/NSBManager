using System.Linq;
using System.Threading;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.Model;
using NServiceBus;

using Endpoint=NSBManager.UserInterface.Models.Endpoint;

namespace NSBManager.UserInterface.PhysicalModule.MessageHandlers
{
    public class BusTopologyChangedEventHandler : IHandleMessages<BusTopologyChangedEvent>
    {
        private readonly IPhysicalModel physicalModel;
        private readonly SynchronizationContext context;

        public BusTopologyChangedEventHandler(IPhysicalModel physicalModel, 
                                              SynchronizationContext context)
        {
            this.physicalModel = physicalModel;
            this.context = context;
        }

        public void Handle(BusTopologyChangedEvent message)
        {
            physicalModel.UpdateModel(message.Endpoints.Select(x => new Endpoint
                                                                        {
                                                                            //todo: add endpoint name here
                                                                            Name = x.Id.ToString()
                                                                        }));

        }
    }
}