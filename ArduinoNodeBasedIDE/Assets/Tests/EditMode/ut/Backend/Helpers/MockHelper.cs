using System.Reflection;
using Backend.Connection;
using Backend.Type;
using Backend.Variables;
using NSubstitute;
using Tests.EditMode.ut.Backend.Mocks;
using Tests.EditMode.ut.Backend.Mocks.IO;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class MockHelper
    {
        public static readonly IType DefaultType = CreateType();

        public static void SetField<T, U>(T owner, string fieldName, U value)
        {
            typeof(T)
                .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance)
                .SetValue(owner, value);
        }

        public static TypeIOMock CreateTypeIO()
        {
            var inOut = Substitute.For<TypeIOMock>();
            inOut.IOType.Returns(IOType.Dynamic);
            inOut.IsOptional.Returns(false);
            return inOut;
        }
        
        public static TypeIOMock CreateClassIO()
        {
            var inOut = Substitute.For<TypeIOMock>();
            inOut.IOType.Returns(IOType.Class);
            inOut.IsOptional.Returns(false);
            return inOut;
        }

        public static AutoIOMock CreateAutoIO()
        {
            var inOut = Substitute.For<AutoIOMock>();
            inOut.IOType.Returns(IOType.Auto);
            inOut.IsOptional.Returns(false);
            return inOut;
        }

        public static FlowIOMock CreateFlowIO()
        {
            var inOut = Substitute.For<FlowIOMock>();
            inOut.IOType.Returns(IOType.Flow);
            inOut.IsOptional.Returns(false);
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

        public static Variable CreateVariable(EType eType = EType.Int)
        {
            var variable = Substitute.For<Variable>();
            var type = CreateType(eType);
            variable.Type.Returns(type);
            return variable;
        }

        public static Variable CreateVariable(IType type)
        {
            var variable = Substitute.For<Variable>();
            variable.Type.Returns(type);
            return variable;
        }
    }
}
