using Backend.Exceptions.InOut;
using Backend.Node;

namespace Backend.InOut.Base
{
    public class FlowInOut : BaseInOut
    {

        private readonly string _name;
        public override IType MyType { get; }
        public override string InOutName => _name;
        
        public FlowInOut(IPlaceHolderNodeType parentNode, InOutSide side, string name) : base(parentNode, side, InOutType.Flow)
        {
            MyType = null;
            _name = name;
        }
        protected override void CheckInOutType(IInOut iInOut)
        {
            base.CheckInOutType(iInOut);
            if (iInOut.InOutType is not InOutType.Flow)        
            {
                throw new WrongConnectionTypeException();
            }
        }

    }
}
