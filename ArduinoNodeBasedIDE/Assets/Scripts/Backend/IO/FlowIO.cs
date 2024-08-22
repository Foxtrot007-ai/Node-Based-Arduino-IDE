using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;

namespace Backend.IO
{
    public class FlowIO : BaseIO
    {
        public override IOType IOType => IOType.Flow;
        public override string IOName { get; }

        public FlowIO(BaseNode parentNode, IOSide side, string name, bool isOptional = false) : base(parentNode, side, isOptional)
        {
            IOName = name;
        }

        protected override void PreCheck(IConnection iConnection)
        {
            if (iConnection is not FlowIO)
            {
                throw new WrongConnectionTypeException();
            }
            base.PreCheck(iConnection);
        }
    }
}
