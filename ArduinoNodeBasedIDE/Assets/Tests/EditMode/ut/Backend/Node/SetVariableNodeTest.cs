using System;
using Backend;
using Backend.Exceptions;
using Backend.Node.BuildIn;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.mocks;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node
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
            _valueMock = Substitute.For<AnyInOutMock>();
            SetInOutMock<VariableNode>(_sut, "_value", _valueMock);
            SetFlowMocks(_sut);
        }
        
        [Test]
        public void ToCodeParamThrowNotImplemented()
        {
            Assert.Throws<NotImplementedException>(() => _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        public void ToCodeThrowInOutMustBeConnectedException()
        {
            MakeUnconnected(_valueMock);

            Assert.Throws<InOutMustBeConnectedException>(() => _sut.ToCode(_codeManagerMock));
        }

        [Test]
        public void ToCodeNewVariableTest()
        {
            MakeFlowConnected();
            MakeConnected(_valueMock);
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Unknown);
            _variableMock.Type.ToCode().Returns("type");
            _variableMock.Name.Returns("test");
            _inOutMock.ParentNode.ToCodeParam(_codeManagerMock).Returns("value");
            
            _sut.ToCode(_codeManagerMock);
            
            _codeManagerMock.Received().AddLine("type test = value;");
            _nextConnectedMock.Received().ParentNode.ToCode(_codeManagerMock);
        }
        
        [Test]
        [TestCase(CodeManager.VariableStatus.Set)]
        [TestCase(CodeManager.VariableStatus.Global)]
        [TestCase(CodeManager.VariableStatus.Param)]
        public void ToCodeOldVariableTest(CodeManager.VariableStatus variableStatus)
        {
            MakeFlowConnected();
            MakeConnected(_valueMock);
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Set);
            _variableMock.Type.ToCode().Returns("type");
            _variableMock.Name.Returns("test");
            _inOutMock.ParentNode.ToCodeParam(_codeManagerMock).Returns("value");
            
            _sut.ToCode(_codeManagerMock);
            
            _codeManagerMock.Received().AddLine("test = value;");
            _nextConnectedMock.Received().ParentNode.ToCode(_codeManagerMock);
        }
    }
}
