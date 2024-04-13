using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.MyType
{
    public class VoidInOut : MyTypeInOut<VoidType>
    {
        public VoidInOut(IPlaceHolderNodeType parentNode, VoidType voidType) 
            : base(parentNode, InOutSide.Output, InOutType.Void, voidType)
        {
        }

        protected override void CheckInOutType(IConnection iConnection)
        {
            throw new WrongConnectionTypeException();
        }
    }
}
