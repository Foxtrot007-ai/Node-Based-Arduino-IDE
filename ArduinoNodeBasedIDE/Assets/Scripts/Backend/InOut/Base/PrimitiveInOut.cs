using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.Base
{
    public class PrimitiveInOut : BaseInOut
    {
        public PrimitiveType ConcretType { get; }

        public override IType MyType => ConcretType;

        public PrimitiveInOut(IPlaceHolderNodeType parentNode, InOutSide side, PrimitiveType primitiveType) : base(parentNode, side, InOutType.Primitive)
        {
            ConcretType = primitiveType;
        }

        protected override void CheckInOutType(IInOut iInOut)
        {
            base.CheckInOutType(iInOut);
            if (iInOut.InOutType is not (InOutType.Primitive or InOutType.String))
            {
                throw new WrongConnectionTypeException();
            }
        }
        
    }
}
