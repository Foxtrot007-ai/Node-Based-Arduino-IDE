using System;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection.MyType
{
    public class AnyInOut : MyTypeInOut<IType>
    {
        public AnyInOut(IPlaceHolderNodeType parentNode, InOutSide side, IType concreteType) : base(parentNode, side, HelperInOut.ETypeToInOut(concreteType.EType), concreteType)
        {
        }
        
        protected virtual void SetMyType(IType iMyType)
        {
            ConcreteType = iMyType ?? throw new ArgumentNullException(null,"Cannot change type to null.");
            InOutType = HelperInOut.ETypeToInOut(iMyType.EType);
        }
        
        public virtual void ChangeMyType(IType iMyType)
        {
            SetMyType(iMyType);
            ReCheck();
        }
        
    }
}