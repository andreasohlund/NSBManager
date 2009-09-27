using System;

namespace NSBManager.Instrumentation.Core.Inspectors.Host
{
    public class ExeHostInspector:IHostInspector
    {
        public Messages.HostInfo GetHostInformation()
        {
            return new Messages.HostInfo { WorkingDir = Environment.CurrentDirectory };
        }
    }
}