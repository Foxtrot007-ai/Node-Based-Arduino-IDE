using Backend.API;
using Backend.Connection.MyType;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection
{
    public class MyTypeInOut : InOut
    {
        protected IType _myType;
        public virtual IType MyType => _myType;
        public override InOutType InOutType => HelperInOut.ETypeToInOut(MyType.EType);
        public override string InOutName => MyType.TypeName;

        public MyTypeInOut(IPlaceHolderNodeType parentNode, InOutSide side, IType myType) : 
            base(parentNode, side)
        {
            _myType = myType;
        }
        
        protected override void PreCheck(IConnection iConnection)
        {
            if (iConnection is not MyTypeInOut)
            {
                throw new WrongConnectionTypeException();
            }
            base.PreCheck(iConnection);
        }

        protected override void Check(InOut input)
        {
            base.Check(input);
            var myTypeInput = (MyTypeInOut)input;
            if (myTypeInput is AutoInOut { MyType: null })
            {
                return;
            }
            CheckCast(myTypeInput);
            CheckAdapter(myTypeInput);
        }
        
        private void CheckCast(MyTypeInOut myTypeInput)
        {

            if (!MyType.CanBeCast(myTypeInput.MyType))
            {
                throw new CannotBeCastException();
            }
        }

        private void CheckAdapter(MyTypeInOut myTypeInput)
        {
            if (MyType.IsAdapterNeed(myTypeInput.MyType))
            {
                throw new AdapterNeedException();
            }
        }
    }
}
