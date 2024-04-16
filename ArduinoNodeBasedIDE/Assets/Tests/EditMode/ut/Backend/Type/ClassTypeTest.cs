using System.Collections.Generic;
using Backend.API;
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

        private static readonly List<IType> _castFalse = new()
        {
            TypeHelper.CreateClassTypeMock("wrong"),
            new PrimitiveType(EType.Int),
            new StringType(),
            new VoidType(),
        };
        
        [Test]
        [TestCaseSource(nameof(_castFalse))]
        public void CanBeCastFalse(IType type)
        {
            //given
            //when
            //then 
            Assert.False(_classType.CanBeCast(type));
        }
        
        private static readonly List<IType> _castTrue = new()
        {
            TypeHelper.CreateClassTypeMock("good"),
        };

        [Test]
        [TestCaseSource(nameof(_castTrue))]
        public void CanBeCastTrue(IType type)
        {
            //given
            //when
            //then
            Assert.True(_classType.CanBeCast(type));
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