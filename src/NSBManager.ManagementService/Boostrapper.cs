using NServiceBus.Host;
using StructureMap;

namespace NSBManager.ManagementService
{
    public class Boostrapper : IWantCustomInitialization
    {
        public void Init()
        {
            ObjectFactory.Configure(x=> x.AddRegistry(new ManagementServiceRegistry()));
        }
    }
}