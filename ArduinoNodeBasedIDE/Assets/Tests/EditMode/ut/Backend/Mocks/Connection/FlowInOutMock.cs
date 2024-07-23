using Backend;
using Backend.Connection;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.Connection
{
    public class FlowInOutMock : FlowInOut
    {
        public InOutMock _connectedMock;
        public FlowInOutMock() : base(Substitute.For<BaseNode>(), InOutSide.Input, "test")
        {
            _connectedMock = Substitute.For<InOutMock>();
        }
        
        public void MakeConnect()
        {
            Connected.Returns(_connectedMock);
        }

        public void MakeDisconnect()
        {
            Connected.Returns((InOut)null);
        }

        public void ExpectToCode(CodeManager codeManager)
        {
            _connectedMock.Received().ParentNode.ToCode(codeManager);
        }
    }
}
