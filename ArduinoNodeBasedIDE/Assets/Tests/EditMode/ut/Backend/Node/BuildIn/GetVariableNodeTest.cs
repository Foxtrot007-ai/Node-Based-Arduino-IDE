using Backend;
using Backend.Exceptions;
using Backend.Node.BuildIn;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{

    [TestFixture]
    [TestOf(nameof(GetVariableNode))]
    [Category("Variable")]
    [Category("Node")]
    public class GetVariableNodeTes : BaseNodeTestSetup
    {
        private VariableNode _sut;
        private Variable _variableMock;

        [SetUp]
        public void Init()
        {
            _variableMock = Substitute.For<Variable>();
            _sut = Substitute.ForPartsOf<GetVariableNode>(_variableMock);

            PrepareBaseSetup(_sut);

            SetInOutMock<VariableNode>("_value", _type1);
            SetInputsList();
            SetOutputsList(_type1);
        }

        [Test]
        public void ToCodeParamTestThrowVariableNotSetException()
        {
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Unknown);
            Assert.Throws<VariableNotSetException>(() => _sut.ToCodeParam(_codeManagerMock));
        }

        [Test]
        public void ToCodeParamGenerateCode()
        {

            _variableMock.Name.Returns("test");
            _codeManagerMock.GetVariableStatus(_variableMock).Returns(CodeManager.VariableStatus.Set);

            Assert.AreEqual("test", _sut.ToCodeParam(_codeManagerMock));
        }
    }
}
