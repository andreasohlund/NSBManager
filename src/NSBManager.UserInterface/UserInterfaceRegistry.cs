using NSBManager.UserInterface.DemoModels;
using NSBManager.UserInterface.PhysicalModule.Model;
using StructureMap.Configuration.DSL;

namespace NSBManager.UserInterface
{
    public class UserInterfaceRegistry : Registry
    {
        public UserInterfaceRegistry()
        {
            For<IPhysicalModel>()
                .Singleton()
                .Use<PhysicalModel>();

            Profile("demo", x => x.For<IPhysicalModel>().Use<FakePhysicalModel>());
        }
    }
}