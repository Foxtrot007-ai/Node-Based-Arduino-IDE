using Backend.Exceptions.InOut;
using Backend.Node;

namespace Backend.Connection
{
    public class FlowInOut : InOut
    {
        public override string InOutName { get; }

        public FlowInOut(IPlaceHolderNodeType parentNode, InOutSide side, string name) : base(parentNode, side, InOutType.Flow)
        {
            InOutName = name;
        }
        
        public override void Reconnect(InOut inOut)
        {
            if (inOut.InOutType is InOutType.Flow)
            {
                base.Reconnect(inOut);
            }
        }
        protected override void Check(InOut inOut)
        {
            base.Check(inOut);
            if (inOut is not FlowInOut)
            {
                throw new WrongConnectionTypeException();
            }
        }
        
    }
}
