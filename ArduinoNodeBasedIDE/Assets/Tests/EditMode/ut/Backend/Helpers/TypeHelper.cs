using System;
using Backend.API;
using Backend.Type;
using Backend.Validator;
using NSubstitute;
using Tests.EditMode.ut.Backend.Type;

namespace Tests.EditMode.ut.Backend.Helpers
{

    public static class TypeHelper
    {
        public static IMyType CreateType(EType eType, string name = null)
        {
            switch (eType)
            {
                case EType.Void:
                    return new VoidType();
                case EType.Class:
                    return CreateClassTypeMock(name);
                case EType.String:
                    return new StringType();
                case EType.Short:
                case EType.Int:
                case EType.Long:
                case EType.LongLong:
                case EType.Float:
                case EType.Double:
                case EType.Bool:
                    return new PrimitiveType(eType);
                default:
                    throw new ArgumentOutOfRangeException(nameof(eType), eType, null);
            }
        }
        public static ClassType CreateClassTypeMock(string name)
        {
            var validator = Substitute.For<IClassTypeValidator>();
            validator.IsClassType(name).Returns(true);
            var classType = Substitute.For<ClassTypeMock>(name, validator);
            return classType;
        }
    }
}