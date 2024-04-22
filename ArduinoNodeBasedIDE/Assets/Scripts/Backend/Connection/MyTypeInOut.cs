using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection
{
    public abstract class MyTypeInOut : InOut
    {
        public abstract IType MyType { get; }
        protected MyTypeInOut(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType) : base(parentNode, side, inOutType)
        {

        }
    }

    public abstract class MyTypeInOut<T> : MyTypeInOut where T : IType
    {
        public T ConcreteType { get; }
        public override string InOutName => ConcreteType.TypeName;

        public override IType MyType => ConcreteType;
        protected MyTypeInOut(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType, T concreteType) : base(parentNode, side, inOutType)
        {
            ConcreteType = concreteType;
        }
        
        protected override void PreCheck(IConnection iConnection)
        {
            if (iConnection is not MyTypeInOut)
            {
                throw new WrongConnectionTypeException();
            }
            base.PreCheck(iConnection);
        }

        protected override void Check(InOut inOut)
        {
            base.Check(inOut);
            CheckInOutType(inOut);
            //TODO CheckAdapter
        }
        
        private void CheckInOutType(InOut inOut)
        {
            var myTypeInOut = (MyTypeInOut)inOut;

            if (!MyType.CanBeCast(myTypeInOut.MyType))
            {
                throw new CannotBeCastException();
            }
        }

    }
}
