using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection
{
    public class MyTypeInOut : InOut
    {
        public IType MyType { get; protected set; }
        public override InOutType InOutType => HelperInOut.ETypeToInOut(MyType.EType);
        public override string InOutName => MyType.TypeName;

        public MyTypeInOut(IPlaceHolderNodeType parentNode, InOutSide side, IType myType) : 
            base(parentNode, side)
        {
            MyType = myType;
        }
        
        protected override void PreCheck(IConnection iConnection)
        {
            base.PreCheck(iConnection);
            if (iConnection is not MyTypeInOut)
            {
                throw new WrongConnectionTypeException();
            }
        }

        protected override void Check(InOut input)
        {
            base.Check(input);
            CheckInOutType(input);
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
