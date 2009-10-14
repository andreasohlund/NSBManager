using System;
using System.Linq.Expressions;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.TestHelpers
{
    public static  class IBusExtensions
    {
        public static void AssertWasPublished<T>(this IBus bus, Expression<Predicate<T>> exp) where T : IMessage
        {
            bus.AssertWasCalled(x => x.Publish(Arg<T[]>
                                                   .Matches(p =>exp.Compile().Invoke(p[0]))));
        }

        public static void AssertWasSent<T>(this IBus bus, Expression<Predicate<T>> exp) where T : IMessage
        {
            bus.AssertWasCalled(x => x.Send(Arg<IMessage[]>
                                                   .Matches(p => exp.Compile().Invoke((T)p[0]))));
        }


        public static void AssertReply<T>(this IBus bus, Expression<Predicate<T>> exp) where T : IMessage
        {
            bus.AssertWasCalled(x => x.Reply(Arg<IMessage[]>
                                                   .Matches(p => exp.Compile().Invoke((T)p[0]))));
        }
    }
}