using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.Base
{
    public class ClassInOut : BaseInOut
    {
        public ClassType ConcreteType { get;  }
        public override IMyType MyType => ConcreteType;

        public ClassInOut(IPlaceHolderNodeType parentNode, InOutSide side, ClassType classType) : base(parentNode, side, InOutType.Class)
        {
            ConcreteType = classType;
        }
        protected override void CheckInOutType(IInOut iInOut)
        {
            base.CheckInOutType(iInOut);
            if (iInOut.InOutType is not InOutType.Class)
            {
                throw new WrongConnectionTypeException();
            }
            
            var classType = ((ClassInOut)iInOut).ConcreteType;
            if (classType != ConcreteType)
            {
                throw new WrongConnectionTypeException();
            }
        }
    }
}
