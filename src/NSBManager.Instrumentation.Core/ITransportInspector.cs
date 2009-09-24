using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation.Core
{
    public interface ITransportInspector
    {
        TransportInfo GetTransportInfo();
    }
}