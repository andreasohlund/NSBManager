using NSBManager.Instrumentation.Core.Inspectors;
using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation
{
    public class GenericHostInspector:IHostInspector
    {
        public HostInformation GetHostInformation()
        {
            return new GenericHostInformation();
        }
    }
}