using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.Base
{
    public class ClassInOut : BaseInOut
    {
        public ClassType ConcretType { get;  }
        public override IType MyType => ConcretType;

        public ClassInOut(IPlaceHolderNodeType parentNode, InOutSide side, ClassType classType) : base(parentNode, side, InOutType.Class)
        {
            ConcretType = classType;
        }
        protected override void CheckInOutType(IInOut iInOut)
        {
            base.CheckInOutType(iInOut);
            if (iInOut.InOutType is not InOutType.Class)
            {
                throw new WrongConnectionTypeException();
            }
            
            var classType = (ClassType) iInOut.MyType;
            if (classType != ConcretType)
            {
                throw new WrongConnectionTypeException();
            }
        }
    }
}
