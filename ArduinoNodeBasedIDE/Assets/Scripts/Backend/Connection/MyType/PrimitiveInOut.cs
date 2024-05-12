using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class PrimitiveInOut : MyTypeInOut
    {
        public PrimitiveInOut(IPlaceHolderNodeType parentNode, InOutSide side, PrimitiveType primitiveType)
            : base(parentNode, side, primitiveType)
        {
        }

    }
}
