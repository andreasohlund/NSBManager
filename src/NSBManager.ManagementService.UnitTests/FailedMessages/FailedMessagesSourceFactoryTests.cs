using NSBManager.ManagementService.FailedMessages;
using NSBManager.ManagementService.FailedMessages.FailedMessageSources;
using NServiceBus.Unicast.Transport;
using NServiceBus.Unicast.Transport.Msmq;
using NUnit.Framework;
using StructureMap;
using NBehave.Spec.NUnit;

namespace NSBManager.ManagementService.UnitTests.FailedMessages
{
    [TestFixture]
    public class FailedMessagesSourceFactoryTests
    {
        [Test]
        public void Resolve_msmq_source()
        {
            
            IContainer container = new Container(x =>
                                              {
                                                  x.AddRegistry(new ManagementServiceRegistry());
                                              });

            IFailedMessagesSourceFactory factory = new FailedMessagesSourceFactory(container,new MsmqTransport());

            var source = factory.CreateFailedMessagesSource("error@server");

            source.ShouldNotBeNull();

            source.ShouldBeInstanceOfType(typeof (MsmqFailedMessagesSource));
        }
    }

    public class FailedMessagesSourceFactory : IFailedMessagesSourceFactory
    {
        private readonly IContainer container;
        private readonly ITransport transport;

        public FailedMessagesSourceFactory(IContainer container,ITransport transport)
        {
            this.container = container;
            this.transport = transport;
        }

        public IFailedMessagesSource CreateFailedMessagesSource(string address)
        {
            return container.With("adress").EqualTo(address)
                .GetInstance<IFailedMessagesSource>(transport.GetType().Name);
        }
    }
} 