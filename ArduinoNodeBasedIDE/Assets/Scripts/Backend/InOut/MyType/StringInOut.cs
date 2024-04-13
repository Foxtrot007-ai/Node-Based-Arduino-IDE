using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.MyType
{
    public class StringInOut : MyTypeInOut<StringType>
    {
        
        
        public StringInOut(IPlaceHolderNodeType parentNode, InOutSide side, StringType stringType) 
            : base(parentNode, side, InOutType.String, stringType)
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
