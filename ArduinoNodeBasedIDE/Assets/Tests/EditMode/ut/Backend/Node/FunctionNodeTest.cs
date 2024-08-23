using System;
using System.Collections.Generic;
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
    [TestOf(nameof(BaseNode))]
    [Category("Node")]
    public class FunctionNodeTest : BaseNodeTestSetup
    {
        private FunctionTemplate _functionTemplateMock;

        private FunctionNode _sut;

        [SetUp]
        public void Init()
        {
            _functionTemplateMock = Substitute.For<FunctionTemplate>();
            _functionTemplateMock.InputsTypes.Returns(new List<IType>());
            var type = MockHelper.CreateType();
            _functionTemplateMock.OutputType.Returns(type);

            _sut = new FunctionNode(_functionTemplateMock);

            PrepareBaseSetup(_sut);
        }

        private void PrepareVoidSetup()
        {
            _functionTemplateMock.OutputType.EType.Returns(EType.Void);
            SetInputsList(_prevMock, _type1, _type2);
            SetOutputsList(_nextMock);
        }

        private void PrepareNonVoidSetup()
        {
            _functionTemplateMock.OutputType.EType.Returns(EType.Int);
            SetInputsList(_type1, _type2);
            SetOutputsList(_typeOut3);
        }

        [Test]
        public void VoidConstructorTest()
        {
            _functionTemplateMock.OutputType.EType.Returns(EType.Void);
            var type1 = MockHelper.CreateType(EType.Int);
            var type2 = MockHelper.CreateType(EType.Bool);
            _functionTemplateMock.InputsTypes.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new FunctionNode(_functionTemplateMock);

            Assert.AreEqual(3, newSut.InputsList.Count);
            Assert.IsInstanceOf<FlowIO>(newSut.InputsList[0]);
            Assert.AreSame(type1, ((TypeIO)newSut.InputsList[1]).MyType);
            Assert.AreSame(type2, ((TypeIO)newSut.InputsList[2]).MyType);

            Assert.AreEqual(1, newSut.OutputsList.Count);
            Assert.IsInstanceOf<FlowIO>(newSut.OutputsList[0]);
        }

        [Test]
        public void NonVoidConstructorTest()
        {
            var outType = MockHelper.CreateType(EType.Int);
            _functionTemplateMock.OutputType.Returns(outType);

            var type1 = MockHelper.CreateType(EType.Int);
            var type2 = MockHelper.CreateType(EType.Bool);
            _functionTemplateMock.InputsTypes.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new FunctionNode(_functionTemplateMock);

            Assert.AreEqual(2, newSut.InputsList.Count);
            Assert.AreSame(type1, ((TypeIO)newSut.InputsList[0]).MyType);
            Assert.AreSame(type2, ((TypeIO)newSut.InputsList[1]).MyType);

            Assert.AreEqual(1, newSut.OutputsList.Count);
            Assert.AreSame(outType, ((TypeIO)newSut.OutputsList[0]).MyType);
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

            _functionTemplateMock.FunctionName.Returns("name");
            _functionTemplateMock.Library.Returns("library");
            _type1.ToCodeParamReturn(_codeManagerMock, "test1");
            _type2.ToCodeParamReturn(_codeManagerMock, "test2");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("name(test1, test2);");
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

            _functionTemplateMock.FunctionName.Returns("name");
            _functionTemplateMock.Library.Returns("library");
            _type1.ToCodeParamReturn(_codeManagerMock, "test1");
            _type2.ToCodeParamReturn(_codeManagerMock, "test2");

            var code = _sut.ToCodeParam(_codeManagerMock);

            Assert.AreEqual("name(test1, test2)", code);
            _codeManagerMock.Received().AddLibrary("library");
        }
    }
}
