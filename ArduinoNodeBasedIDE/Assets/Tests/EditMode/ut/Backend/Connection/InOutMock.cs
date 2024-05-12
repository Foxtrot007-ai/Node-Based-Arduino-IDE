using Backend.Connection;
using Backend.Node;

namespace Tests.EditMode.ut.Backend.Connection
{
    public class InOutMock : InOut
    {
        public override InOutType InOutType { get; }
        public override string InOutName { get; }
        public InOutMock(IPlaceHolderNodeType parentNode, InOutSide side) : base(parentNode, side)
        {
        }

    }
}