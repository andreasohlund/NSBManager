using System;
using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation.Core.Inspectors.Host
{
    public class ExeHostInspector:IHostInspector
    {
        public HostInfo GetHostInformation()
        {
            return new HostInfo { WorkingDir = Environment.CurrentDirectory };
        }
    }
}