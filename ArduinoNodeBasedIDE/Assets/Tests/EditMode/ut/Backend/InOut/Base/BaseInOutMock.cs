using Backend;
using Backend.InOut;
using Backend.InOut.Base;
using Backend.Node;

namespace Tests.EditMode.ut.Backend.InOut.Base
{
    public class BaseInOutMock : BaseInOut
    {
        public BaseInOutMock(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType) : base(parentNode, side, inOutType)
        {
        }

        public override IType MyType { get; }
    }
}