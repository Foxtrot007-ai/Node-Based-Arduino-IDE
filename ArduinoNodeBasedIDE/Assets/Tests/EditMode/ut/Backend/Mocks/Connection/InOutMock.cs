using Backend.Connection;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.Connection
{
    public class InOutMock : InOut
    {
        public override InOutType InOutType { get; }
        public override string InOutName { get; }
        public InOutMock(BaseNodeMock parentNode, InOutSide side) : base(parentNode, side)
        {
        }

        public InOutMock() : base(Substitute.For<BaseNode>(), InOutSide.Input)
        {
            
        }
    }
}