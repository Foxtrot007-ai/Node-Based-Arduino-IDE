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
    [TestOf(nameof(GetVariableNode))]
    [Category("Variable")]
    [Category("Node")]
    public class GetVariableNodeTes : BaseNodeTestSetup
    {
        private VariableNode _sut;
        private VariableMock _variableMock;
        private AnyInOutMock _valueMock;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _variableMock = Substitute.For<VariableMock>();
            _sut = Substitute.ForPartsOf<GetVariableNode>(_variableMock);
            _valueMock = CreateAnyInOutMock();
            SetInOutMock<VariableNode>(_sut, "_value", _valueMock);
        }

        [Test]
        public void ToCodeThrowNotImplemented()
        {
            Assert.Throws<NotImplementedException>(() => _sut.ToCode(_codeManagerMock));
        }

        [Test]
        public void ToCodeParamThrowInOutMustBeConnectedException()
        {
            _valueMock.MakeDisconnect();

            Assert.Throws<InOutMustBeConnectedException>(() => _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        public void ToCodeParamTestThrowVariableNotSetException()
        {
            _valueMock.MakeConnect();
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Unknown);

            Assert.Throws<VariableNotSetException>(() => _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        public void ToCodeParamGenerateCode()
        {
            _valueMock.MakeConnect();
            _variableMock.Name.Returns("test");
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Set);

            Assert.AreEqual("test", _sut.ToCodeParam(_codeManagerMock));
        }
    }
}
