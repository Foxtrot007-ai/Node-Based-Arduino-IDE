using System.Collections.Generic;
using Backend.Exceptions;
using Backend.Type;
using Backend.Validator;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Type
{
    [TestFixture]
    [TestOf(typeof(ClassType))]
    [Category("Type")]
    public class ClassTypeTest
    {
        private IClassTypeValidator _validator;
        private ClassType _classType;

        [OneTimeSetUp]
        public void Setup()
        {
            _classType = TypeHelper.CreateClassTypeMock("good");
            _validator = Substitute.For<IClassTypeValidator>();
            _validator.IsClassType(Arg.Any<string>()).Returns(false);
            _validator.IsClassType("test").Returns(true);
            _validator.IsClassType("test1").Returns(true);
            _validator.IsClassType("test2").Returns(true);
        }

        [Test]
        public void NotClassNameException()
        {
            //given
            //when
            var exception =
                Assert.Throws<NotClassTypeException>(() => _ = new ClassTypeMock("NotTest", _validator));
            //then
            Assert.AreEqual("\"NotTest\" is not class type.", exception.Message);
        }

        [Test]
        public void ClassNameOk()
        {
            //given
            const string name = "test";
            //when
            var classType = new ClassTypeMock(name, _validator);
            //then
            Assert.AreEqual(name, classType.TypeName);
        }

        private static readonly List<(IType, bool)> _castParam = new()
        {
            (TypeHelper.CreateClassTypeMock("wrong"), false),
            (new PrimitiveType(EType.Int), false),
            (new StringType(), false),
            (new VoidType(), false),
            (TypeHelper.CreateClassTypeMock("good"), true),
        };

        [Test]
        [TestCaseSource(nameof(_castParam))]
        public void CanBeCastParamTest((IType, bool) param)
        {
            //given
            var (type, expect) = param;
            //when
            //then 
            Assert.AreEqual(expect, _classType.CanBeCast(type));
        }

        [Test]
        public void NotEqual()
        {
            //given
            var classType1 = new ClassTypeMock("test1", _validator);
            var classType2 = new ClassTypeMock("test2", _validator);
            //when
            //then
            Assert.AreNotEqual(classType1, classType2);
            Assert.False(classType1 == classType2);
        }

        [Test]
        public void Equal()
        {
            //given
            var classType1 = new ClassTypeMock("test1", _validator);
            var classType2 = new ClassTypeMock("test1", _validator);
            //when
            //then
            Assert.AreEqual(classType1, classType2);
            Assert.True(classType1 == classType2);
        }
    }
}
