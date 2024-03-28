using Backend.Exceptions;
using Backend.Type;
using Backend.Validator;
using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Type
{
    [TestFixture]
    [TestOf(typeof(ClassType))]
    [Category("Type")]
    public class ClassTypeTest
    {
        private IClassTypeValidator _validator;

        [OneTimeSetUp]
        public void Setup()
        {
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
                Assert.Throws<NotClassNameException>(() => new ClassTypeMock("NotTest", _validator));
            //then
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