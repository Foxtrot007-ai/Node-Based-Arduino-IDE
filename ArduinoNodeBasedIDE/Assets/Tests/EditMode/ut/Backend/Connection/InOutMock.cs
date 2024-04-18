using Backend.Connection;
using Backend.Node;

namespace Tests.EditMode.ut.Backend.Connection
{
    public class InOutMock : InOut
    {
        public override string InOutName { get; }
        public InOutMock(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType) : base(parentNode, side, inOutType)
        {
        }

    }
}