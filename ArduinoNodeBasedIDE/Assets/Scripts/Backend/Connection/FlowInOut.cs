using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;

namespace Backend.Connection
{
    public class FlowInOut : InOut
    {
        public override InOutType InOutType => InOutType.Flow;
        public override string InOutName { get; }

        public FlowInOut(BaseNode parentNode, InOutSide side, string name, bool isOptional = false) : base(parentNode, side, isOptional)
        {
            InOutName = name;
        }
        
        protected override void PreCheck(IConnection iConnection)
        {
            if (iConnection is not FlowInOut)
            {
                throw new WrongConnectionTypeException();
            }
            base.PreCheck(iConnection);
        }
        
    }
}
