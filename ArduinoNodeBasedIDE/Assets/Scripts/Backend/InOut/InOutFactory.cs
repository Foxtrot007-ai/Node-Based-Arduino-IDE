using Backend.InOut.Base;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut
{
    public static class InOutFactory
    {
        public static BaseInOut CreateBaseInOut(IType type, IPlaceHolderNodeType parent, InOutSide side)
        {
            return type switch
            {
                VoidType voidType => new VoidInOut(parent, voidType),
                ClassType classType => new ClassInOut(parent, side, classType),
                StringType stringType => new StringInOut(parent, side, stringType),
                PrimitiveType primitiveType => new PrimitiveInOut(parent, side, primitiveType),
                _ => null
            };
        }

        public static BaseInOut CreateBaseInOut(IType iType, IInOut inOut)
        {
            return CreateBaseInOut(iType, inOut.ParentNode, inOut.Side);
        }
    }
}