using System;
using System.Collections.Generic;
using Backend;
using Backend.API;
using Backend.Node.BuildIn;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.mocks;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{

    [TestFixture]
    [TestOf(nameof(SetVariableNode))]
    [Category("Variable")]
    [Category("Node")]
    public class SetVariableNodeTest : BaseNodeTestSetup
    {
        private VariableNode _sut;
        private VariableMock _variableMock;
        private AnyInOutMock _valueMock;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _variableMock = Substitute.For<VariableMock>();
            _sut = Substitute.ForPartsOf<SetVariableNode>(_variableMock);
            _valueMock = CreateAnyInOutMock();
            PrepareSetup();
        }

        void PrepareSetup()
        {
            SetInOutMock<VariableNode>(_sut, "_value", _valueMock);
            SetFlowMocks(_sut);
            SetInputsList(_sut, new List<IConnection> { _prevMock, _valueMock });
            SetOutputsList(_sut, new List<IConnection> { _nextMock });

            MakeFlowConnected();
            _valueMock.MakeConnect();
        }
        
        [Test]
        public void ToCodeNewVariableTest()
        {
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Unknown);
            _variableMock.Type.ToCode().Returns("type");
            _variableMock.Name.Returns("test");
            _valueMock.ToCodeParamReturn(_codeManagerMock, "value");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("type test = value;");
            ExpectNextToCode();
        }

        [Test]
        [TestCase(CodeManager.VariableStatus.Set)]
        [TestCase(CodeManager.VariableStatus.Global)]
        [TestCase(CodeManager.VariableStatus.Param)]
        public void ToCodeOldVariableTest(CodeManager.VariableStatus variableStatus)
        {
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Set);
            _variableMock.Type.ToCode().Returns("type");
            _variableMock.Name.Returns("test");
            _valueMock.ToCodeParamReturn(_codeManagerMock, "value");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("test = value;");
            ExpectNextToCode();
        }
    }
}
