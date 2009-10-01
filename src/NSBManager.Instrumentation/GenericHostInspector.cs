using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation
{
    public class GenericHostInspector:IHostInspector
    {
        public HostInfo GetHostInformation()
        {
            return new GenericHostInfo();
        }
    }
}