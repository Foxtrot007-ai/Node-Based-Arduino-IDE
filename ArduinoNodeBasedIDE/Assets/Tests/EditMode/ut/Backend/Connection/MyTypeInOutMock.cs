using Backend.Connection;
using Backend.Node;
using Backend.Type;

namespace Tests.EditMode.ut.Backend.Connection
{
    public class MyTypeInOutMock : MyTypeInOut<IType>
    {

        public MyTypeInOutMock(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType, IType concreteType) : base(parentNode, side, inOutType, concreteType)
        {
        }
        
    } 
}
