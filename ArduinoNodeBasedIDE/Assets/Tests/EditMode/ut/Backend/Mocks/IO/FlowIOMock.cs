using Backend;
using Backend.Connection;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.IO
{
    public class FlowIOMock : FlowIO
    {
        public BaseIOMock _connectedMock;
        public FlowIOMock() : base(Substitute.For<BaseNode>(), IOSide.Input, "test")
        {
            _connectedMock = Substitute.For<BaseIOMock>();
        }
        
        public void MakeConnect()
        {
            Connected.Returns(_connectedMock);
        }

        public void MakeDisconnect()
        {
            Connected.Returns((BaseIO)null);
        }

        public void ExpectToCode(CodeManager codeManager)
        {
            _connectedMock.Received().ParentNode.ToCode(codeManager);
        }
    }
}
