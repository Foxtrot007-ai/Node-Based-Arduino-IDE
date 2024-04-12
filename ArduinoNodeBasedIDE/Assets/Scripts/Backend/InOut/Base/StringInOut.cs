using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.Base
{
    public class StringInOut : BaseInOut
    {
        
        public StringType ConcreteType { get; }
        public override IMyType MyType => ConcreteType;



        public StringInOut(IPlaceHolderNodeType parentNode, InOutSide side, StringType stringType) : base(parentNode, side, InOutType.String)
        {
            ConcreteType = stringType;
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
