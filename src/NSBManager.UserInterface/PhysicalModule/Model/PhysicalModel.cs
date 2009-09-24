using System.Collections.Generic;
using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.PhysicalModule.Events;
using Endpoint = NSBManager.UserInterface.Models.Endpoint;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public class PhysicalModel : IPhysicalModel
    {
        private readonly IEventAggregator eventAggregator;

        public PhysicalModel(IEventAggregator eventAggregator)
        {
            endpoints = new List<Endpoint>();
            this.eventAggregator = eventAggregator;
        }

        private readonly IList<Endpoint> endpoints;

        public IEnumerable<Endpoint> Endpoints
        {
            get { return endpoints; }
        }

        public void UpdateModel(IEnumerable<Endpoint> currentEndpoints)
        {
            endpoints.Clear();

            foreach (var endpoint in currentEndpoints)
            {
                endpoints.Add(endpoint);
            }

            eventAggregator.Publish<PhysicalModelChanged>();
        }


    }

}