using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.Base
{
    public class VoidInOut : BaseInOut
    {
        public VoidType ConcretType { get; }
        public override IType MyType => ConcretType;

        public VoidInOut(IPlaceHolderNodeType parentNode, VoidType voidType) : base(parentNode, InOutSide.Output, InOutType.Void)
        {
            ConcretType = voidType;
        }

        protected override void CheckInOutType(IInOut iInOut)
        {
            throw new WrongConnectionTypeException();
        }
    }
}
