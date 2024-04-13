using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.MyType
{
    public class PrimitiveInOut : MyTypeInOut<PrimitiveType>
    {
        
        public PrimitiveInOut(IPlaceHolderNodeType parentNode, InOutSide side, PrimitiveType primitiveType) 
            : base(parentNode, side, InOutType.Primitive, primitiveType)
        {
        }

        protected override void CheckInOutType(IConnection iConnection)
        {
            if (iConnection.InOutType is not (InOutType.Primitive or InOutType.String))
            {
                throw new WrongConnectionTypeException();
            }
        }
        
    }
}
