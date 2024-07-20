using System.Collections.Generic;
using Backend.API;
using Backend.Node.BuildIn;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.mocks;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(VariableNode))]
    [Category("Variable")]
    [Category("Node")]
    public class VariableNodeTest : BaseNodeTestSetup
    {
        private VariableMock _variableMock;
        private VariableNodeMock _sut;

        [SetUp]
        public override void Init()
        {
            base.Init();
            _variableMock = Substitute.For<VariableMock>();
            _sut = Substitute.ForPartsOf<VariableNodeMock>(_variableMock);
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
            var list = new List<IConnection> { _inOutMock };
            _sut.Configure().InputsList.Returns(list);

            _sut.Delete();

            _variableMock.Received().DeleteRef(_sut);
            _inOutMock.Received().Delete();
            Assert.True(_sut.IsDeleted);
        }
    }
}
