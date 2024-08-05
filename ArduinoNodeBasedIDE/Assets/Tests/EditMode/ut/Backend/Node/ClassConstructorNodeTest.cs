using System.Collections.Generic;
using Backend.Connection;
using Backend.IO;
using Backend.Node;
using Backend.Template;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(ClassConstructorNode))]
    [Category("Node")]
    public class ClassConstructorNodeTest : BaseNodeTestSetup
    {
        private ClassConstructorTemplate _classConstructorTemplateMock;
        private ClassType _classTypeMock;

        private ClassConstructorNode _sut;

        [SetUp]
        public void Init()
        {
            _classTypeMock = MockHelper.CreateClassType("Test");

            _classConstructorTemplateMock = Substitute.For<ClassConstructorTemplate>();
            _classConstructorTemplateMock.Inputs.Returns(new List<IType>());
            _classConstructorTemplateMock.Class.Returns(_classTypeMock);

            _sut = new ClassConstructorNode(_classConstructorTemplateMock);

            PrepareBaseSetup(_sut);
        }

        private void PrepareSetup()
        {
            SetInputsList(_type1, _type2);
            SetOutputsList(_class1);

            MakeAllConnected();
        }

        [Test]
        public void NonVoidConstructorTest()
        {
            _classConstructorTemplateMock.Class.Returns(_classTypeMock);

            var type1 = MockHelper.CreateType(EType.Int);
            var type2 = MockHelper.CreateType(EType.Bool);
            _classConstructorTemplateMock.Inputs.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new ClassConstructorNode(_classConstructorTemplateMock);

            Assert.AreEqual(2, newSut.InputsList.Count);
            Assert.AreSame(type1, ((TypeIO)newSut.InputsList[0]).MyType);
            Assert.AreSame(type2, ((TypeIO)newSut.InputsList[1]).MyType);

            Assert.AreEqual(1, newSut.OutputsList.Count);
            Assert.AreSame(_classTypeMock, ((TypeIO)newSut.OutputsList[0]).MyType);
        }

        [Test]
        public void ToCodeParamTest()
        {
            PrepareSetup();

            _classConstructorTemplateMock.Library.Returns("library");
            _classConstructorTemplateMock.Class.ToCode().Returns("Class");

            _type1.ToCodeParamReturn(_codeManagerMock, "test1");
            _type2.ToCodeParamReturn(_codeManagerMock, "test2");

            var code = _sut.ToCodeParam(_codeManagerMock);

            Assert.AreEqual("Class(test1, test2)", code);
            _codeManagerMock.Received().AddLibrary("library");
        }
    }
}
