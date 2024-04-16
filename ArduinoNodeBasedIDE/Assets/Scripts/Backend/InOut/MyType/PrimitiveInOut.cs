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
    }
}
