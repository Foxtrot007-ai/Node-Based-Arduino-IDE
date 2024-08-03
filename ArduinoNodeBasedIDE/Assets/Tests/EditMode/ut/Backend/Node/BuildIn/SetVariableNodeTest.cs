using Backend;
using Backend.Node.BuildIn;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;

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
            _sut = Substitute.ForPartsOf<SetVariableNode>(_variableMock, "test");

            PrepareBaseSetup(_sut);

            SetInOutMock<VariableNode>("_value", _type1);
            SetInputsList(_prevMock, _type1);
            SetOutputsList(_nextMock);
        }

        [Test]
        public void ToCodeNewVariableTest()
        {
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Unknown);
            _variableMock.Type.ToCode().Returns("type");
            _variableMock.Name.Returns("test");
            _type1.ToCodeParamReturn(_codeManagerMock, "value");

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
            _type1.ToCodeParamReturn(_codeManagerMock, "value");

            _sut.ToCode(_codeManagerMock);

            _codeManagerMock.Received().AddLine("test = value;");
            ExpectNextToCode();
        }
    }
}
