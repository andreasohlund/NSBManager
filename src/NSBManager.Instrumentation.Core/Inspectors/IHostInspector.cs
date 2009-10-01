using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation.Core.Inspectors
{
    public interface IHostInspector
    {
        HostInfo GetHostInformation();
    }
}