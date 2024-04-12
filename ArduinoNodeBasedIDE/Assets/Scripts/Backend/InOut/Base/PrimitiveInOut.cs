using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.Base
{
    public class PrimitiveInOut : BaseInOut
    {
        public PrimitiveType ConcreteType { get; }

        public override IMyType MyType => ConcreteType;

        public PrimitiveInOut(IPlaceHolderNodeType parentNode, InOutSide side, PrimitiveType primitiveType) : base(parentNode, side, InOutType.Primitive)
        {
            ConcreteType = primitiveType;
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
