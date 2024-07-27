using Backend;
using Backend.Connection;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.IO
{
    public class TypeIOMock : TypeIO
    {
        public BaseIOMock _connectedMock;
        public TypeIOMock() : base(Substitute.For<BaseNode>(), IOSide.Output, null)
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

        public void ToCodeParamReturn(CodeManager codeManager, string code)
        {
            _connectedMock.ParentNode.ToCodeParam(codeManager).Returns(code);
        }
    }
}
