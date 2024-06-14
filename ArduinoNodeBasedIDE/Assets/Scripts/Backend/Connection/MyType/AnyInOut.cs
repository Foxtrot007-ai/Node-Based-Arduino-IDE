using System;
using Backend.Exceptions;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class AnyInOut : MyTypeInOut
    {
        public AnyInOut(BaseNode parentNode, InOutSide side, IType concreteType) : base(parentNode, side, concreteType)
        {
        }
        public virtual void ChangeMyType(IType iMyType)
        {
            if (iMyType is null)
            {
                throw new ArgumentNullException(null, "Cannot change type to null.");
            }
            
            if (Side == InOutSide.Input && iMyType.EType == EType.Void)
            {
                throw new WrongTypeException("Cannot change type to void for input side.");
            }
            _myType = iMyType;
            ReCheck();
        }
    }
}