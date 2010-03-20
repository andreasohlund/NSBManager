using System;

namespace NSBManager.Instrumentation.Core.Messages
{
    [Serializable]
    public class HostInformation
    {
        public string WorkingDir { get; set; }

        public HostType Type { get; set; }
    }
}