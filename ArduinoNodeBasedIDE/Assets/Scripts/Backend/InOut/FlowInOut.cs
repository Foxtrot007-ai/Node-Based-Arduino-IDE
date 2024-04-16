using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;

namespace Backend.InOut
{
    public class FlowInOut : BaseInOut
    {
        public override string InOutName { get; }

        public FlowInOut(IPlaceHolderNodeType parentNode, InOutSide side, string name) : base(parentNode, side, InOutType.Flow)
        {
            InOutName = name;
        }
        
        public override void Reconnect(IInOut inOut)
        {
            if (inOut.InOutType is InOutType.Flow)
            {
                base.Reconnect(inOut);
            }
        }
        protected override void Check(IInOut inOut)
        {
            base.Check(inOut);
            if (inOut is not FlowInOut)
            {
                throw new WrongConnectionTypeException();
            }
        }
    }
}
