using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.Connection
{
    public class TypeInOut : InOut
    {
        protected IType _myType;
        public virtual IType MyType => _myType;
        public override InOutType InOutType => HelperInOut.ETypeToInOut(MyType.EType);
        public override string InOutName => MyType.TypeName;

        public TypeInOut(BaseNode parentNode, InOutSide side, IType myType, bool isOptional = false) : 
            base(parentNode, side, isOptional)
        {
            _myType = myType;
        }
        
        protected override void PreCheck(IConnection iConnection)
        {
            if (iConnection is not TypeInOut)
            {
                throw new WrongConnectionTypeException();
            }
            base.PreCheck(iConnection);
        }

        protected override void Check(InOut input)
        {
            base.Check(input);
            var typeInput = (TypeInOut)input;
            if (typeInput is AutoInOut { MyType: null })
            {
                return;
            }
            CheckCast(typeInput);
            CheckAdapter(typeInput);
        }
        
        private void CheckCast(TypeInOut typeInput)
        {

            if (!MyType.CanBeCast(typeInput.MyType))
            {
                throw new CannotBeCastException();
            }
        }

        private void CheckAdapter(TypeInOut typeInput)
        {
            if (MyType.IsAdapterNeed(typeInput.MyType))
            {
                throw new AdapterNeedException();
            }
        }
    }
}
