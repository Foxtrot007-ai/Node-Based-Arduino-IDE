using System.Reflection;
using Backend.Connection;
using Backend.IO;
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

        public static FieldInfo GetField<T>(T owner, string fieldName)
        {
            return typeof(T)
                .GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        public static void SetFieldValue<T, U>(T owner, string fieldName, U value)
        {
            GetField<T>(owner, fieldName)
                .SetValue(owner, value);
        }

        public static U GetFieldValue<T, U>(T owner, string fieldName)
        {
            return (U)GetField<T>(owner, fieldName)
                .GetValue(owner);
        }

        public static PropertyInfo GetProperty<T>(T owner, string fieldName)
        {
            return typeof(T)
                .GetProperty(fieldName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        }
        
        public static void SetPropertyValue<T, U>(T owner, string fieldName, U value)
        {
            GetProperty<T>(owner, fieldName)
                .SetValue(owner, value, null);
        }

        public static U GetPropertyValue<T, U>(T owner, string fieldName)
        {
            return (U)GetProperty<T>(owner, fieldName)
                .GetValue(owner, null);
        }
        
        public static TypeIOMock CreateTypeIO(IOSide side = IOSide.Input)
        {
            var inOut = Substitute.For<TypeIOMock>(side);
            inOut.IOType.Returns(IOType.Primitive);
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
