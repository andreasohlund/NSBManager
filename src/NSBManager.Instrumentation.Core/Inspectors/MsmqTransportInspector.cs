using NSBManager.Instrumentation.Core.Messages;
using NServiceBus.Unicast.Transport.Msmq;

namespace NSBManager.Instrumentation.Core.Inspectors
{
    public class MsmqTransportInspector:ITransportInspector
    {
        private readonly MsmqTransport transport;

        public MsmqTransportInspector(MsmqTransport transport)
        {
            this.transport = transport;
        }

        public TransportInfo GetTransportInfo()
        {
            return new TransportInfo
                       {
                           Type = TransportTypes.Msmq,
                           Adress = transport.Address
                       };
        }
    }
}