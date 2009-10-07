using System;
using NServiceBus;
using NServiceBus.Host;

namespace NSBManager.ManagementService
{
    public class EndpointConfig:IConfigureThisEndpoint,
                                AsA_Publisher, IWantCustomInitialization
        
    {
        public void Init()
        {
            Configure.With().StructureMapBuilder().BinarySerializer();
        }
    }

   
}