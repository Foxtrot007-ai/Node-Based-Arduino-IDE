using Backend.Node.BuildIn;
using Backend.Variables;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.mocks;

namespace Tests.EditMode.ut.Backend.Node.BuildIn
{
    [TestFixture]
    [TestOf(nameof(VariableNode))]
    [Category("Variable")]
    [Category("Node")]
    public class VariableNodeTest : BaseNodeTestSetup
    {
        private Variable _variableMock;
        private VariableNodeMock _sut;

        [SetUp]
        public void Init()
        {
            _variableMock = Substitute.For<Variable>();
            _sut = Substitute.ForPartsOf<VariableNodeMock>(_variableMock);

            PrepareBaseSetup(_sut);
        }

        [Test]
        public void ConstructorTest()
        {
            var newSut = new VariableNodeMock(_variableMock);

            _variableMock.Received().AddRef(newSut);
        }

        [Test]
        public void DeleteVariableNodeTest()
        {
            SetInputsList(_type1);

            _sut.Delete();

            _variableMock.Received().DeleteRef(_sut);
            _type1.Received().Delete();
            Assert.True(_sut.IsDeleted);
        }
    }
}
