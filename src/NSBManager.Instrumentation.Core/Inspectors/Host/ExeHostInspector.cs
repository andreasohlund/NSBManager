using System;
using NSBManager.Instrumentation.Core.Messages;

namespace NSBManager.Instrumentation.Core.Inspectors.Host
{
    public class ExeHostInspector:IHostInspector
    {
        public HostInformation GetHostInformation()
        {
            return new HostInformation
                       {
                           Type=HostType.CommandLineExe,
                           WorkingDir = Environment.CurrentDirectory
                       };
        }
    }
}