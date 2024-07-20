using System.Reflection;
using Backend;
using Backend.Connection;
using Backend.Node;
using NSubstitute;
using NUnit.Framework;
using Tests.EditMode.ut.Backend.Mocks.Connection;

namespace Tests.EditMode.ut.Backend.Node
{
    [TestFixture]
    public class BaseNodeTestSetup
    {
        protected CodeManager _codeManagerMock;
        protected FlowInOutMock _prevMock;
        protected FlowInOutMock _prevConnectedMock;
        protected FlowInOutMock _nextMock;
        protected FlowInOutMock _nextConnectedMock;
        protected InOutMock _inOutMock;

        [SetUp]
        public virtual void Init()
        {
            _codeManagerMock = Substitute.For<CodeManager>();
            _prevMock = Substitute.For<FlowInOutMock>();
            _prevConnectedMock = Substitute.For<FlowInOutMock>();
            _nextMock = Substitute.For<FlowInOutMock>();
            _nextConnectedMock = Substitute.For<FlowInOutMock>();
            _inOutMock = Substitute.For<InOutMock>();
        }

        public void SetFlowMocks(BaseNode baseNode)
        {
            SetInOutMock<BaseNode>(baseNode, "_prevNode", _prevMock);
            SetInOutMock<BaseNode>(baseNode, "_nextNode", _nextMock);
        }

        public void SetInOutMock<T>(BaseNode node, string name, InOut inOutMock)
        {
            typeof(T).GetField(name, BindingFlags.NonPublic | BindingFlags.Instance).SetValue(node, inOutMock);
        }

        public void MakeUnconnected(InOut inOut)
        {
            inOut.Connected.Returns((InOut)null);
            inOut.ConnectedInOut.Returns((InOut)null);
        }

        public void MakeConnected(InOut inOut, InOut inOut2)
        {
            inOut.Connected.Returns(inOut2);
            inOut.ConnectedInOut.Returns(inOut2);
        }
        public void MakeConnected(InOut inOut)
        {
            MakeConnected(inOut, _inOutMock);
        }

        public void MakeFlowConnected()
        {
            MakeConnected(_prevMock, _prevConnectedMock);
            MakeConnected(_nextMock, _nextConnectedMock);
        }
    }
}
