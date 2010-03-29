using Machine.Specifications;
using StructureMap.AutoMocking;

namespace NSBManager.ManagementService.UnitTests.EndpointControl
{
    public abstract class ContextSpecification<TSubjectUnderTest>
        where TSubjectUnderTest : class
    {
        private static IAutoMockingContainer<TSubjectUnderTest> _autoMockingContainer;
        protected static TSubjectUnderTest SUT { get; set; }

        Establish context = () =>
                                {
                                    _autoMockingContainer = new StructureMapAMC<TSubjectUnderTest>();
                                    SUT = _autoMockingContainer.Create();
                                };

        Cleanup stuff = () =>
                            {
                                SUT = null;
                                _autoMockingContainer = null;
                            };

        protected static TDependency Dependency<TDependency>()
            where TDependency : class
        {
            return _autoMockingContainer.GetMock<TDependency>();
        }

        protected static TStub Stub<TStub>()
            where TStub : class
        {
            return _autoMockingContainer.GetStub<TStub>();
        }
    }


    public interface IAutoMockingContainer<TSubject>
        where TSubject : class
    {
        TSubject Create();
        TMock GetMock<TMock>() where TMock : class;
        TStub GetStub<TStub>() where TStub : class;
    }

    public class StructureMapAMC<TSubject>
        : IAutoMockingContainer<TSubject>
        where TSubject : class
    {
        private readonly RhinoAutoMocker<TSubject> _rhinoAutoMocker;

        public StructureMapAMC()
        {
            _rhinoAutoMocker =
                new RhinoAutoMocker<TSubject>(MockMode.AAA);
        }

        public TSubject Create()
        {
            return _rhinoAutoMocker.ClassUnderTest;
        }

        public TMock GetMock<TMock>()
            where TMock : class
        {
            return GetDependency<TMock>();
        }

        public TStub GetStub<TStub>()
            where TStub : class
        {
            return GetDependency<TStub>();
        }

        private TDependency GetDependency<TDependency>()
            where TDependency : class
        {
            return _rhinoAutoMocker.Get<TDependency>();
        }
    }
}