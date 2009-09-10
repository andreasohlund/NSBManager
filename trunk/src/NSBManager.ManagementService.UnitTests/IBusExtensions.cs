using System;
using System.Linq.Expressions;
using NServiceBus;
using Rhino.Mocks;

namespace NSBManager.ManagementService.UnitTests
{
    public static  class IBusExtensions
    {
        public static void AssertWasPublished<T>(this IBus bus, Expression<Predicate<T>> exp) where T : IMessage
        {
            bus.AssertWasCalled(x => x.Publish(Arg<T[]>
                                                   .Matches(p =>exp.Compile().Invoke(p[0]))));
        }

    }
}