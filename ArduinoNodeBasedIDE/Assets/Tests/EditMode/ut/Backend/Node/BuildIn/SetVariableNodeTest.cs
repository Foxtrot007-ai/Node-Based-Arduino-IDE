using System.Collections.Generic;
using Backend;
using Backend.API;
using Backend.Node.BuildIn;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.mocks;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{

    [TestFixture]
    [TestOf(nameof(SetVariableNode))]
    [Category("Variable")]
    [Category("Node")]
    public class SetVariableNodeTest : BaseNodeTestSetup
    {
        private VariableNode _sut;
        private Variable _variableMock;

        [SetUp]
        public void Init()
        {
            _variableMock = Substitute.For<Variable>();
            _sut = Substitute.ForPartsOf<SetVariableNode>(_variableMock);

            PrepareBaseSetup(_sut);

            SetInOutMock<VariableNode>("_value", _any1);
            SetInputsList(_prevMock, _any1);
            SetOutputsList(_nextMock);
        }

        [Test]
        public void ToCodeNewVariableTest()
        {
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Unknown);
            _variableMock.Type.ToCode().Returns("type");
            _variableMock.Name.Returns("test");
            _any1.ToCodeParamReturn(_codeManagerMock, "value");

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
            _any1.ToCodeParamReturn(_codeManagerMock, "value");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("test = value;");
            ExpectNextToCode();
        }
    }
}
