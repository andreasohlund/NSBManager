using NServiceBus;
using NServiceBus.Host;

namespace NSBManager.Instrumentation
{
    public class Configuration : IWantCustomInitialization
    {
        public void Init()
        {
            Configure.Instance.EnableInstrumentation();
        }
    }
}