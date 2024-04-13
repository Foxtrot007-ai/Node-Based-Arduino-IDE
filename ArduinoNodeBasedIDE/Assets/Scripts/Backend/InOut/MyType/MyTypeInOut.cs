using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;

namespace Backend.InOut.MyType
{
    public abstract class MyTypeInOut : BaseInOut
    {
        public abstract IMyType MyType { get; }
        protected MyTypeInOut(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType) : base(parentNode, side, inOutType)
        {
            
        }
    }
    
    public abstract class MyTypeInOut<T> : MyTypeInOut where T : IMyType
    {
        public T ConcreteType { get; protected set; }
        public override string InOutName => ConcreteType.TypeName;

        public override IMyType MyType => ConcreteType;
        protected MyTypeInOut(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType, T concreteType) : base(parentNode, side, inOutType)
        {
            ConcreteType = concreteType;
        }

        public override void Connect(IConnection iConnection)
        {
            CheckInOutType(iConnection);
            //TODO CheckAdapter
            base.Connect(iConnection);
        }

        public override void Reconnect(IInOut inOut)
        {
            try
            {
                CheckInOutType(inOut);
                //TODO CheckAdapter
                base.Reconnect(inOut);
            }
            catch(InOutException)
            {
            }

        }
        protected abstract void CheckInOutType(IConnection iConnection);
    }
}
