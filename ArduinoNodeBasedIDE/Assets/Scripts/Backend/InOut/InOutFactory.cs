using Backend.API;
using Backend.InOut.MyType;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut
{
    public static class InOutFactory
    {
        public static MyTypeInOut CreateBaseInOut(IMyType myType, IPlaceHolderNodeType parent, InOutSide side)
        {
            return myType switch
            {
                VoidType voidType => new VoidInOut(parent, voidType),
                ClassType classType => new ClassInOut(parent, side, classType),
                StringType stringType => new StringInOut(parent, side, stringType),
                PrimitiveType primitiveType => new PrimitiveInOut(parent, side, primitiveType),
                _ => null
            };
        }

        public static BaseInOut CreateBaseInOut(IMyType iMyType, IInOut inOut)
        {
            return CreateBaseInOut(iMyType, inOut.ParentNode, inOut.Side);
        }
    }
}