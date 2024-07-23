using Backend;
using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Node;
using Backend.Type;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.Connection
{
    public class AnyInOutMock : AnyInOut
    {
        public InOutMock _connectedMock;
        public AnyInOutMock() : base(Substitute.For<BaseNode>(), InOutSide.Output, Substitute.For<IType>())
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
