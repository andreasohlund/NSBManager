using NServiceBus;
using NServiceBus.Host;
using NServiceBus.ObjectBuilder;

namespace NSBManager.Instrumentation
{
    public class Configuration : IWantCustomInitialization
    {
        public void Init()
        {
            //Configure the custom host inspector
            Configure.Instance.EnableInstrumentation(new NServiceBusGenericHostInspector());

            
        }
    }
}