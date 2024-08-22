using Backend;
using Backend.IO;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.IO
{
    public class TypeIOMock : TypeIO
    {
        public BaseIOMock _connectedMock;

        public TypeIOMock(IOSide side = IOSide.Input) : base(Substitute.For<BaseNode>(), side, null)
        {
            _connectedMock = Substitute.For<BaseIOMock>();
        }
        public TypeIOMock() : base(Substitute.For<BaseNode>(), IOSide.Input, null)
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
