using Backend.API;
using Backend.Exceptions.InOut;
using Backend.Node;
using Backend.Type;

namespace Backend.InOut.MyType
{
    public class ClassInOut : MyTypeInOut<ClassType>
    {
        public ClassInOut(IPlaceHolderNodeType parentNode, InOutSide side, ClassType classType)
            : base(parentNode, side, InOutType.Class, classType)
        {
        }
        protected override void CheckInOutType(IConnection iConnection)
        {
            if (iConnection.InOutType is not InOutType.Class)
            {
                throw new WrongConnectionTypeException();
            }

            var classType = ((ClassInOut)iConnection).ConcreteType;
            if (classType != ConcreteType)
            {
                throw new WrongConnectionTypeException();
            }
        }
    }
}
