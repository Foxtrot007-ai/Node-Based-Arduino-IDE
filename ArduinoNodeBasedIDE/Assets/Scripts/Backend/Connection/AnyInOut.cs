using System;
using Backend.Exceptions;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection
{
    public class AnyInOut : TypeInOut
    {
        public AnyInOut(BaseNode parentNode, InOutSide side, IType concreteType, bool isOptional = false) : base(parentNode, side, concreteType, isOptional)
        {
        }
        public virtual void ChangeMyType(IType iType)
        {
            if (iType is null)
            {
                throw new ArgumentNullException(null, "Cannot change type to null.");
            }
            
            if (Side == InOutSide.Input && iType.EType == EType.Void)
            {
                throw new WrongTypeException("Cannot change type to void for input side.");
            }
            _myType = iType;
            ReCheck();
        }
    }
}