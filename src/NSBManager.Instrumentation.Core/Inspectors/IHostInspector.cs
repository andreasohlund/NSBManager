namespace NSBManager.Instrumentation.Core.Inspectors
{
    public interface IHostInspector
    {
        Messages.HostInfo GetHostInformation();
    }
}