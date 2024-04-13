using Backend.InOut;
using Backend.Node;

namespace Tests.EditMode.ut.Backend.InOut
{
    public class BaseInOutMock : BaseInOut
    {
        public BaseInOutMock(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType) : base(parentNode, side, inOutType)
        {
        }

        public override string InOutName { get; }
    }
}