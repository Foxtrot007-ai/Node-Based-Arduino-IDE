using Backend.Connection;
using Backend.Type;
using NSubstitute;
using Tests.EditMode.ut.Backend.Mocks;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class MockHelper
    {
        public static readonly IType DefaultType = CreateType();
        public static AnyInOutMock CreateAnyInOut()
        {
            var inOut = Substitute.For<AnyInOutMock>();
            inOut.InOutType.Returns(InOutType.Dynamic);
            return inOut;
        }

        public static ClassInOutMock CreateClassInOut()
        {
            var inOut = Substitute.For<ClassInOutMock>();
            inOut.InOutType.Returns(InOutType.Class);
            return inOut;
        }

        public static AutoInOutMock CreateAutoInOut()
        {
            var inOut = Substitute.For<AutoInOutMock>();
            inOut.InOutType.Returns(InOutType.Auto);
            return inOut;
        }

        public static FlowInOutMock CreateFLowInOut()
        {
            var inOut = Substitute.For<FlowInOutMock>();
            inOut.InOutType.Returns(InOutType.Flow);
            return inOut;
        }

        public static IType CreateType(EType eType = EType.Int)
        {
            var type = Substitute.For<IType>();
            type.EType.Returns(eType);
            return type;
        }

        public static ClassTypeMock CreateClassType(string name = "")
        {
            var classType = Substitute.For<ClassTypeMock>();
            classType.EType.Returns(EType.Class);
            classType.TypeName.Returns(name);

            return classType;
        }
    }
}
