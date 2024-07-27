using System;
using System.Collections.Generic;
using Backend.Connection;
using Backend.Node;
using Backend.Template;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(ClassMethodNode))]
    [Category("Node")]
    public class ClassMethodNodeTest : BaseNodeTestSetup
    {
        private ClassMethodTemplate _classMethodTemplate;
        private ClassType _classTypeMock;

        private ClassMethodNode _sut;

        [SetUp]
        public void Init()
        {
            _classTypeMock = MockHelper.CreateClassType("Test");

            _classMethodTemplate = Substitute.For<ClassMethodTemplate>();
            _classMethodTemplate.InputsTypes.Returns(new List<IType>());
            _classMethodTemplate.Class.Returns(_classTypeMock);
            var type = MockHelper.CreateType(EType.Int);
            _classMethodTemplate.OutputType.Returns(type);

            _sut = new ClassMethodNode(_classMethodTemplate);
            PrepareBaseSetup(_sut);
            SetInOutMock<ClassMethodNode>("_classIn", _class1);
        }

        private void PrepareVoidSetup()
        {
            _classMethodTemplate.OutputType.EType.Returns(EType.Void);
            SetInputsList(_prevMock, _class1, _any1, _any2);
            SetOutputsList(_nextMock);
        }

        private void PrepareNonVoidSetup()
        {
            _classMethodTemplate.OutputType.EType.Returns(EType.Int);
            SetInputsList(_class1, _any1, _any2);
            SetOutputsList(_any3);
        }

        [Test]
        public void VoidConstructorTest()
        {
            _classMethodTemplate.Class.Returns(_classTypeMock);
            _classMethodTemplate.OutputType.EType.Returns(EType.Void);
            var type1 = MockHelper.CreateType(EType.Int);
            var type2 = MockHelper.CreateType(EType.Bool);
            _classMethodTemplate.InputsTypes.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new ClassMethodNode(_classMethodTemplate);

            Assert.AreEqual(4, newSut.InputsList.Count);
            Assert.IsInstanceOf<FlowInOut>(newSut.InputsList[0]);
            Assert.AreSame(_classTypeMock, ((TypeInOut)newSut.InputsList[1]).MyType);
            Assert.AreSame(type1, ((AnyInOut)newSut.InputsList[2]).MyType);
            Assert.AreSame(type2, ((AnyInOut)newSut.InputsList[3]).MyType);

            Assert.AreEqual(1, newSut.OutputsList.Count);
            Assert.IsInstanceOf<FlowInOut>(newSut.OutputsList[0]);
        }

        [Test]
        public void NonVoidConstructorTest()
        {
            _classMethodTemplate.Class.Returns(_classTypeMock);
            var outType = MockHelper.CreateType(EType.Int);
            _classMethodTemplate.OutputType.Returns(outType);

            var type1 = MockHelper.CreateType(EType.Int);
            var type2 = MockHelper.CreateType(EType.Bool);
            _classMethodTemplate.InputsTypes.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new ClassMethodNode(_classMethodTemplate);

            Assert.AreEqual(3, newSut.InputsList.Count);
            Assert.AreSame(_classTypeMock, ((TypeInOut)newSut.InputsList[0]).MyType);
            Assert.AreSame(type1, ((AnyInOut)newSut.InputsList[1]).MyType);
            Assert.AreSame(type2, ((AnyInOut)newSut.InputsList[2]).MyType);

            Assert.AreEqual(1, newSut.OutputsList.Count);
            Assert.AreSame(outType, ((AnyInOut)newSut.OutputsList[0]).MyType);
        }

        [Test]
        public void VoidToCodeParamThrowsTest()
        {
            PrepareVoidSetup();
            Assert.Throws<NotImplementedException>(() => _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        public void VoidToCodeTest()
        {
            PrepareVoidSetup();

            _classMethodTemplate.Name.Returns("name");
            _classMethodTemplate.Library.Returns("library");

            _class1.ToCodeParamReturn(_codeManagerMock, "class");
            _any1.ToCodeParamReturn(_codeManagerMock, "test1");
            _any2.ToCodeParamReturn(_codeManagerMock, "test2");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("class.name(test1, test2);");
            _codeManagerMock.Received().AddLibrary("library");
            ExpectNextToCode();
        }

        [Test]
        public void NonVoidToCodeThrowsTest()
        {
            PrepareNonVoidSetup();
            Assert.Throws<NotImplementedException>(() => _sut.ToCode(_codeManagerMock));
        }

        [Test]
        public void NonVoidToCodeTest()
        {
            PrepareNonVoidSetup();

            _classMethodTemplate.Name.Returns("name");
            _classMethodTemplate.Library.Returns("library");

            _class1.ToCodeParamReturn(_codeManagerMock, "class");
            _any1.ToCodeParamReturn(_codeManagerMock, "test1");
            _any2.ToCodeParamReturn(_codeManagerMock, "test2");

            var code = _sut.ToCodeParam(_codeManagerMock);

            Assert.AreEqual("class.name(test1, test2)", code);
            _codeManagerMock.Received().AddLibrary("library");
        }
    }
}
