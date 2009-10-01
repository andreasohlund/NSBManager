using System;
using System.Collections.Generic;
using NSBManager.UserInterface.PhysicalModule.Model;

namespace NSBManager.UserInterface.DemoModels
{
    public class FakePhysicalModel:IPhysicalModel
    {
        private readonly IList<Endpoint> endpoints;

        public FakePhysicalModel()
        {
            endpoints = new List<Endpoint>();
            for (int i = 0; i < 20; i++)
            {
                endpoints.Add(new Endpoint
                                  {
                                      Name = string.Format("endpoint{0}@server{1}", i, i)
                                  });
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