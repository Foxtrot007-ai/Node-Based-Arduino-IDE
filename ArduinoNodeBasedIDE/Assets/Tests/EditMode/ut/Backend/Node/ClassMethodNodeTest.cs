using System;
using System.Collections.Generic;
using Backend.API;
using Backend.Connection;
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
    [TestOf(nameof(ClassMethodNode))]
    [Category("Node")]
    public class ClassMethodNodeTest : BaseNodeTestSetup
    {
        private ClassMethodTemplate _classMethodTemplate;
        private ClassInOutMock _classInMock;
        private ClassType _classTypeMock;
        private AnyInOutMock _in1, _in2, _out;

        private ClassMethodNode _sut;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _in1 = CreateAnyInOutMock();
            _in2 = CreateAnyInOutMock();
            _out = CreateAnyInOutMock();
            _classInMock = CreateClassInOutMock();
            _classTypeMock = TypeHelper.CreateClassTypeMock("Test");

            _classMethodTemplate = Substitute.For<ClassMethodTemplate>();
            _classMethodTemplate.InputsTypes.Returns(new List<IType>());
            _classMethodTemplate.Class.Returns(_classTypeMock);
            var type = TypeHelper.CreateMyTypeMock(EType.Int);
            _classMethodTemplate.OutputType.Returns(type);

            _sut = new ClassMethodNode(_classMethodTemplate);
            SetInOutMock<ClassMethodNode>(_sut, "_classIn", _classInMock);
        }

        private void MakeParamConnected()
        {
            _classInMock.MakeConnect();
            _in1.MakeConnect();
            _in2.MakeConnect();
            _out.MakeConnect();
        }
        private void PrepareVoidSetup()
        {
            SetFlowMocks(_sut);
            _classMethodTemplate.OutputType.EType.Returns(EType.Void);
            SetInputsList(_sut, new List<IConnection> { _prevMock, _classInMock, _in1, _in2 });
            SetOutputsList(_sut, new List<IConnection> { _nextMock });
        }

        private void PrepareNonVoidSetup()
        {
            _classMethodTemplate.OutputType.EType.Returns(EType.Int);
            SetInputsList(_sut, new List<IConnection> { _classInMock, _in1, _in2 });
            SetOutputsList(_sut, new List<IConnection> { _out });
        }

        [Test]
        public void VoidConstructorTest()
        {
            _classMethodTemplate.Class.Returns(_classTypeMock);
            _classMethodTemplate.OutputType.EType.Returns(EType.Void);
            var type1 = TypeHelper.CreateMyTypeMock(EType.Int);
            var type2 = TypeHelper.CreateMyTypeMock(EType.Bool);
            _classMethodTemplate.InputsTypes.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new ClassMethodNode(_classMethodTemplate);

            Assert.AreEqual(4, newSut.InputsList.Count);
            Assert.IsInstanceOf<FlowInOut>(newSut.InputsList[0]);
            Assert.AreSame(_classTypeMock, ((ClassInOut)newSut.InputsList[1]).MyType);
            Assert.AreSame(type1, ((AnyInOut)newSut.InputsList[2]).MyType);
            Assert.AreSame(type2, ((AnyInOut)newSut.InputsList[3]).MyType);

            Assert.AreEqual(1, newSut.OutputsList.Count);
            Assert.IsInstanceOf<FlowInOut>(newSut.OutputsList[0]);
        }

        [Test]
        public void NonVoidConstructorTest()
        {
            _classMethodTemplate.Class.Returns(_classTypeMock);
            var outType = TypeHelper.CreateMyTypeMock(EType.Int);
            _classMethodTemplate.OutputType.Returns(outType);

            var type1 = TypeHelper.CreateMyTypeMock(EType.Int);
            var type2 = TypeHelper.CreateMyTypeMock(EType.Bool);
            _classMethodTemplate.InputsTypes.Returns(new List<IType>
            {
                type1,
                type2,
            });

            var newSut = new ClassMethodNode(_classMethodTemplate);

            Assert.AreEqual(3, newSut.InputsList.Count);
            Assert.AreSame(_classTypeMock, ((ClassInOut)newSut.InputsList[0]).MyType);
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
            MakeFlowConnected();
            MakeParamConnected();

            _classMethodTemplate.Name.Returns("name");
            _classMethodTemplate.Library.Returns("library");

            _classInMock.ToCodeParamReturn(_codeManagerMock, "class");
            _in1.ToCodeParamReturn(_codeManagerMock, "test1");
            _in2.ToCodeParamReturn(_codeManagerMock, "test2");

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
            MakeParamConnected();

            _classMethodTemplate.Name.Returns("name");
            _classMethodTemplate.Library.Returns("library");

            _classInMock.ToCodeParamReturn(_codeManagerMock, "class");
            _in1.ToCodeParamReturn(_codeManagerMock, "test1");
            _in2.ToCodeParamReturn(_codeManagerMock, "test2");

            var code = _sut.ToCodeParam(_codeManagerMock);

            Assert.AreEqual("class.name(test1, test2)", code);
            _codeManagerMock.Received().AddLibrary("library");
        }
    }
}
