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
        
        public override void Connect(IConnection iConnection)
        {
            if (iConnection.InOutType is not InOutType.Flow)
            {
                throw new WrongConnectionTypeException();
            }
            base.Connect(iConnection);
        }
        public override void Reconnect(IInOut inOut)
        {
            if (inOut.InOutType is InOutType.Flow)
            {
                base.Reconnect(inOut);
            }
        }
    }
}
