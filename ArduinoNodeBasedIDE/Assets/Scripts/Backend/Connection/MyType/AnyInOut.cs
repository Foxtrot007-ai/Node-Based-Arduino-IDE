using System;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class AnyInOut : MyTypeInOut
    {
        public AnyInOut(IPlaceHolderNodeType parentNode, InOutSide side, IType concreteType) : base(parentNode, side, concreteType)
        {
        }
        public virtual void ChangeMyType(IType iMyType)
        {
            MyType = iMyType ?? throw new ArgumentNullException(null, "Cannot change type to null.");
            ReCheck();
        }
    }
}