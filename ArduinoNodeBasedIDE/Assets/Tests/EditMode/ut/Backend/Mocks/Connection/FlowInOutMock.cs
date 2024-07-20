using Backend.Connection;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.Connection
{
    public class FlowInOutMock : FlowInOut
    {

        public FlowInOutMock() : base(Substitute.For<BaseNode>(), InOutSide.Input, "test")
        {
            
        }
    }
}
