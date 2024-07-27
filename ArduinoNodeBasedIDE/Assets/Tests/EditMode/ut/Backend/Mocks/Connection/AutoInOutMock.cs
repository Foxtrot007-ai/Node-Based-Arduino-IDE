using Backend;
using Backend.Connection;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.Connection
{
    public class AutoInOutMock : AutoInOut
    {
        public InOutMock _connectedMock;

        public AutoInOutMock() : base(Substitute.For<BaseNode>(), InOutSide.Input)
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

        public void ToCodeParamReturn(CodeManager codeManager, string code)
        {
            _connectedMock.ParentNode.ToCodeParam(codeManager).Returns(code);
        }
    }
}
