using NServiceBus;

namespace NSBManager.ManagementService
{
    public class EndpointConfig:IConfigureThisEndpoint,
                                AsA_Publisher, IWantCustomInitialization
        
    {
        public void Init()
        {
            Configure.With()
                .StructureMapBuilder()
                .BinarySerializer();
        }
    }

   
}