using Backend.Exceptions;
using Backend.IO;
using Backend.Node;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks.IO;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    [TestOf(nameof(BaseNode))]
    [Category("Node")]
    public class BaseNodeTest : BaseNode
    {

        private BaseIOMock _baseIOMock;

        [SetUp]
        public void Init()
        {
            InputsList.Clear();
            OutputsList.Clear();
            _prevNode = Substitute.For<FlowIOMock>();
            _prevNode.Connected.Returns(_prevNode);
            _nextNode = Substitute.For<FlowIOMock>();
            _nextNode.Connected.Returns(_nextNode);
            _baseIOMock = Substitute.For<BaseIOMock>();
            _baseIOMock.IOType.Returns(IOType.Primitive);
        }

        [Test]
        public void CheckToCodeThrowTest()
        {
            AddInputs(_prevNode, _baseIOMock);
            _baseIOMock.Connected.Returns((BaseIO)null);

            var exception = Assert.Throws<InOutMustBeConnectedException>(CheckToCode);
            
            Assert.AreSame(_baseIOMock, exception.InOut);
        }
        [Test]
        public void CheckoToCodeNotThrowForOptional()
        {
            AddInputs(_prevNode, _baseIOMock);
            _baseIOMock.IsOptional.Returns(true);
            _baseIOMock.Connected.Returns((BaseIO)null);
            
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
            AddInputs(_baseIOMock);
            AddOutputs(_baseIOMock);

            AddFlowInputs();

            Assert.AreEqual(2, InputsList.Count);
            Assert.AreEqual(2, OutputsList.Count);
            Assert.AreSame(_prevNode, InputsList[0]);
            Assert.AreSame(_nextNode, OutputsList[0]);
            Assert.AreSame(_baseIOMock, InputsList[1]);
            Assert.AreSame(_baseIOMock, OutputsList[1]);

            RemoveFlowInputs();

            _prevNode.Received().Disconnect();
            _nextNode.Received().Disconnect();
            Assert.AreEqual(1, InputsList.Count);
            Assert.AreEqual(1, OutputsList.Count);
            Assert.AreSame(_baseIOMock, InputsList[0]);
            Assert.AreSame(_baseIOMock, OutputsList[0]);
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
            AddInputs(_baseIOMock);
            AddOutputs(_baseIOMock);
            RemoveInOut(_baseIOMock);

            Assert.AreEqual(0, InputsList.Count);
            Assert.AreEqual(1, OutputsList.Count);
            _baseIOMock.Received().Delete();
        }

        [Test]
        public void GetWithoutFlowTest()
        {
            AddFlowInputs();
            AddInputs(_baseIOMock);

            var list = GetWithoutFlow(InputsList);
            Assert.AreEqual(1, list.Count);
            Assert.AreSame(_baseIOMock, list[0]);
        }

        [Test]
        public void GetWithoutFlowNoFlowInListTest()
        {
            AddInputs(_baseIOMock);

            var list = GetWithoutFlow(InputsList);
            Assert.AreEqual(1, list.Count);
            Assert.AreSame(_baseIOMock, list[0]);
        }
    }
}
