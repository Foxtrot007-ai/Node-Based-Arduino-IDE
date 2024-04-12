using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.Base
{
    public class VoidInOut : BaseInOut
    {
        public VoidType ConcreteType { get; }
        public override IMyType MyType => ConcreteType;

        public VoidInOut(IPlaceHolderNodeType parentNode, VoidType voidType) : base(parentNode, InOutSide.Output, InOutType.Void)
        {
            ConcreteType = voidType;
        }

        protected override void CheckInOutType(IInOut iInOut)
        {
            throw new WrongConnectionTypeException();
        }
    }
}
