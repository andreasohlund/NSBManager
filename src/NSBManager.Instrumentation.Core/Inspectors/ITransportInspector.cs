using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation.Core.Inspectors
{
    public interface ITransportInspector
    {
        TransportInfo GetTransportInformation();
    }
}