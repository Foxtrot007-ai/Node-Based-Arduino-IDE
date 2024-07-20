using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Node;
using Backend.Type;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.Connection
{
    public class AnyInOutMock : AnyInOut
    {
        public AnyInOutMock() : base(Substitute.For<BaseNode>(), InOutSide.Output, Substitute.For<IType>())
        {
        }
    }
}
