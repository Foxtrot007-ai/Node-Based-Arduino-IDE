using System;
using Backend.Type;
using Backend.Validator;
using NSubstitute;
using Tests.EditMode.ut.Backend.Mocks;

namespace Tests.EditMode.ut.Backend.Helpers
{
    public static class TypeHelper
    {
        public static readonly IType DefaultType = CreateMyTypeMock();
        public static ClassType CreateClassTypeMock(string name)
        {
            var validator = Substitute.For<IClassTypeValidator>();
            validator.IsClassType(name).Returns(true);
            var classType = Substitute.For<ClassTypeMock>(name, validator);
            return classType;
        }

        public static IType CreateMyTypeMock(EType eType)
        {
            var typeMock = Substitute.For<IType>();
            typeMock.EType.Returns(eType);
            return typeMock;
        }
        public static IType CreateMyTypeMock()
        {
            var typeMock = Substitute.For<IType>();
            typeMock.EType.Returns(EType.Int);
            return typeMock;
        }
    }
}