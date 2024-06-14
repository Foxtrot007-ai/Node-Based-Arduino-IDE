using Backend.Exceptions;
using Backend.Node;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(BaseNode))]
    [Category("Node")]
    public class BaseNodeTest : BaseNode
    {
        
        protected override void CheckToCode()
        {
            throw new System.NotImplementedException();
        }
        public override string ToCode()
        {
            throw new System.NotImplementedException();
        }

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
        }
        
        [Test]
        public void CheckIfConnectedShouldNotThrowWhenConnected()
        {
            Assert.DoesNotThrow(() => CheckIfConnected(_prevNode));
        }
        
        [Test]
        public void CheckConnectedExceptionWhenNoConnectedTest()
        {
            _inOutMock.Connected.ReturnsNull();

            var exception = Assert.Throws<InOutMustBeConnectedException>(() => CheckIfConnected(_inOutMock));
        }
        
        [Test]
        public void CheckFLowConnectedTest()
        {
            CheckFlowConnected();

            _prevNode.Connected.Received();
            _nextNode.Connected.Received();
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
    }
}
