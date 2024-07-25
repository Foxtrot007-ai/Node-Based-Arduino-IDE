using System;
using System.Collections.Generic;
using Backend.API;
using Backend.Connection.MyType;
using Backend.Node;
using Backend.Template;
using Backend.Type;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Helpers;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(ClassConstructorNode))]
    [Category("Node")]
    public class ClassConstructorNodeTest : BaseNodeTestSetup
    {
        private ClassConstructorTemplate _classConstructorTemplateMock;
        private ClassInOutMock _classOutMock;
        private ClassType _classTypeMock;
        private AnyInOutMock _in1, _in2;

        private ClassConstructorNode _sut;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _in1 = CreateAnyInOutMock();
            _in2 = CreateAnyInOutMock();
            _classOutMock = CreateClassInOutMock();
            _classTypeMock = TypeHelper.CreateClassTypeMock("Test");

            _classConstructorTemplateMock = Substitute.For<ClassConstructorTemplate>();
            _classConstructorTemplateMock.Inputs.Returns(new List<IType>());
            _classConstructorTemplateMock.Class.Returns(_classTypeMock);

            _sut = new ClassConstructorNode(_classConstructorTemplateMock);
        }

        private void MakeParamConnected()
        {
            _classOutMock.MakeConnect();
            _in1.MakeConnect();
            _in2.MakeConnect();
        }

        private void PrepareSetup()
        {
            SetInputsList(_sut, new List<IConnection> { _in1, _in2 });
            SetOutputsList(_sut, new List<IConnection> { _classOutMock });
            
            MakeParamConnected();
        }

        [Test]
        public void NonVoidConstructorTest()
        {
            _classConstructorTemplateMock.Class.Returns(_classTypeMock);

            var type1 = TypeHelper.CreateMyTypeMock(EType.Int);
            var type2 = TypeHelper.CreateMyTypeMock(EType.Bool);
            _classConstructorTemplateMock.Inputs.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new ClassConstructorNode(_classConstructorTemplateMock);

            Assert.AreEqual(2, newSut.InputsList.Count);
            Assert.AreSame(type1, ((AnyInOut)newSut.InputsList[0]).MyType);
            Assert.AreSame(type2, ((AnyInOut)newSut.InputsList[1]).MyType);

            Assert.AreEqual(1, newSut.OutputsList.Count);
            Assert.AreSame(_classTypeMock, ((ClassInOut)newSut.OutputsList[0]).MyType);
        }

        [Test]
        public void ToCodeParamTest()
        {
            PrepareSetup();

            _classConstructorTemplateMock.Library.Returns("library");
            _classConstructorTemplateMock.Class.ToCode().Returns("Class");

            _in1.ToCodeParamReturn(_codeManagerMock, "test1");
            _in2.ToCodeParamReturn(_codeManagerMock, "test2");

            var code = _sut.ToCodeParam(_codeManagerMock);

            Assert.AreEqual("new Class(test1, test2)", code);
            _codeManagerMock.Received().AddLibrary("library");
        }
    }
}
