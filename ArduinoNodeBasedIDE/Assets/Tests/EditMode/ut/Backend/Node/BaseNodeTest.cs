using Backend.Connection;
using Backend.Exceptions;
using Backend.Node;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(BaseNode))]
    [Category("Node")]
    public class BaseNodeTest : BaseNode
    {

        private InOutMock _inOutMock;

        [SetUp]
        public void Init()
        {
            InputsList.Clear();
            OutputsList.Clear();
            _prevNode = Substitute.For<FlowInOutMock>();
            _prevNode.Connected.Returns(_prevNode);
            _nextNode = Substitute.For<FlowInOutMock>();
            _nextNode.Connected.Returns(_nextNode);
            _inOutMock = Substitute.For<InOutMock>();
            _inOutMock.InOutType.Returns(InOutType.Primitive);
        }

        [Test]
        public void CheckToCodeThrowTest()
        {
            AddInputs(_prevNode, _inOutMock);
            _inOutMock.Connected.Returns((InOut)null);

            var exception = Assert.Throws<InOutMustBeConnectedException>(CheckToCode);
            
            Assert.AreSame(_inOutMock, exception.InOut);
        }
        [Test]
        public void CheckoToCodeNotThrowForOptional()
        {
            AddInputs(_prevNode, _inOutMock);
            _inOutMock.IsOptional.Returns(true);
            _inOutMock.Connected.Returns((InOut)null);
            
            CheckToCode();
        }
        
        [Test]
        public void RemoveFlowNothingToDo()
        {
            RemoveFlowInputs();

            _nextNode.DidNotReceive().Disconnect();
            _prevNode.DidNotReceive().Disconnect();
        }

        [Test]
        public void AddAndRemoveFlowOnlyFlowInList()
        {
            AddFlowInputs();

            Assert.AreEqual(1, InputsList.Count);
            Assert.AreEqual(1, OutputsList.Count);
            Assert.AreSame(_prevNode, InputsList[0]);
            Assert.AreSame(_nextNode, OutputsList[0]);

            RemoveFlowInputs();

            _prevNode.Received().Disconnect();
            _nextNode.Received().Disconnect();
            Assert.AreEqual(0, InputsList.Count);
            Assert.AreEqual(0, OutputsList.Count);
        }

        [Test]
        public void AddAndRemoveFlowOtherOnList()
        {
            AddInputs(_inOutMock);
            AddOutputs(_inOutMock);

            AddFlowInputs();

            Assert.AreEqual(2, InputsList.Count);
            Assert.AreEqual(2, OutputsList.Count);
            Assert.AreSame(_prevNode, InputsList[0]);
            Assert.AreSame(_nextNode, OutputsList[0]);
            Assert.AreSame(_inOutMock, InputsList[1]);
            Assert.AreSame(_inOutMock, OutputsList[1]);

            RemoveFlowInputs();

            _prevNode.Received().Disconnect();
            _nextNode.Received().Disconnect();
            Assert.AreEqual(1, InputsList.Count);
            Assert.AreEqual(1, OutputsList.Count);
            Assert.AreSame(_inOutMock, InputsList[0]);
            Assert.AreSame(_inOutMock, OutputsList[0]);
        }

        [Test]
        public void DeleteTest()
        {
            AddFlowInputs();

            Delete();
            _nextNode.Received().Delete();
            _prevNode.Received().Delete();
        }

        [Test]
        public void RemoveInOutTest()
        {
            AddInputs(_inOutMock);
            AddOutputs(_inOutMock);
            RemoveInOut(_inOutMock);

            Assert.AreEqual(0, InputsList.Count);
            Assert.AreEqual(1, OutputsList.Count);
            _inOutMock.Received().Delete();
        }

        [Test]
        public void GetWithoutFlowTest()
        {
            AddFlowInputs();
            AddInputs(_inOutMock);

            var list = GetWithoutFlow(InputsList);
            Assert.AreEqual(1, list.Count);
            Assert.AreSame(_inOutMock, list[0]);
        }

        [Test]
        public void GetWithoutFlowNoFlowInListTest()
        {
            AddInputs(_inOutMock);

            var list = GetWithoutFlow(InputsList);
            Assert.AreEqual(1, list.Count);
            Assert.AreSame(_inOutMock, list[0]);
        }
    }
}
