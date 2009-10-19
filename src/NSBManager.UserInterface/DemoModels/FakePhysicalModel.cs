using System;
using System.Collections.Generic;
using NSBManager.ManagementService.Messages;
using NSBManager.UserInterface.PhysicalModule.Model;

namespace NSBManager.UserInterface.DemoModels
{
    public class FakePhysicalModel:IPhysicalModel
    {
        private readonly IList<Endpoint> endpoints;

        public FakePhysicalModel()
        {
            endpoints = new List<Endpoint>();
            GenerateServersWithEndpoints(8, 10);
        }

        private void GenerateServersWithEndpoints(int nrOfServers, int maxNrOfEndpoints)
        {
            var random = new Random();
            var nrOfEndpoints = random.Next(1, maxNrOfEndpoints);

            for (int s = 1; s < nrOfServers + 1; s++)
            {
                for (int e = 1; e < nrOfEndpoints + 1; e++)
                {
                    endpoints.Add(new Endpoint
                                      {
                                          Id = string.Format("endpoint{0}@server{1}", e,s),
                                          ServerName = string.Format("server{0}", s)
                                      });
                }
                nrOfEndpoints = random.Next(1, maxNrOfEndpoints);
            }
        }

        public IEnumerable<Endpoint> Endpoints
        {
            get
            {
                return endpoints;
            }
        }

        public void UpdateModel(IEnumerable<Endpoint> endpoints)
        {
            
        }
    }
}