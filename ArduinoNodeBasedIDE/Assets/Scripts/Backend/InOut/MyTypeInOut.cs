using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut
{
    public abstract class MyTypeInOut : BaseInOut
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
        protected override void Check(IInOut inOut)
        {
            base.Check(inOut);
            CheckInOutType(inOut);
        }
        private void CheckInOutType(IInOut inOut)
        {
            if (inOut is not MyTypeInOut myTypeInOut)
            {
                throw new WrongConnectionTypeException();
            }

            if (!MyType.CanBeCast(myTypeInOut.MyType))
            {
                throw new CannotBeCastException();
            }
        }

    }
}
