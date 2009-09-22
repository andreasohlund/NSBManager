using System;
using System.Collections.Generic;
using NSBManager.UserInterface.Events;
using NSBManager.UserInterface.Infrastructure;

namespace NSBManager.UserInterface.Models
{
    public class PhysicalModel
    {
        private readonly IEventAggregator aggregator;

        public PhysicalModel(IEventAggregator aggregator)
        {
            this.aggregator = aggregator;
        }

        public void UpdateModel(IEnumerable<Endpoint> endpoints)
        {

            aggregator.Publish(new PhysicalModelChanged{Endpoints = endpoints});
        }
    }
}