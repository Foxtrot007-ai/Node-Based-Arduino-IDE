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
    }
}
