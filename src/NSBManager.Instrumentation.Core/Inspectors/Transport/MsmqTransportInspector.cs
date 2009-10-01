using System;
using NSBManager.Instrumentation.Core.Messages;
using NServiceBus.Unicast.Transport.Msmq;
using NServiceBus.Utils;

namespace NSBManager.Instrumentation.Core.Inspectors.Transport
{
    public class MsmqTransportInspector:ITransportInspector
    {
        private readonly MsmqTransport transport;

        public MsmqTransportInspector(MsmqTransport transport)
        {
            this.transport = transport;
        }

        public TransportInfo GetTransportInformation()
        {
            return new MsmqTransportInfo
                       {
                           Adress = MsmqUtilities.GetQueueNameFromLogicalName(transport.InputQueue) + "@" +MsmqUtilities.GetMachineNameFromLogicalName(transport.InputQueue)
                       };
        }
    }
    [Serializable]
    public class MsmqTransportInfo : TransportInfo
    {
    }
}