using Backend.API;
using Backend.InOut;
using Backend.InOut.MyType;
using Backend.Node;
using Backend.Type;

namespace Tests.EditMode.ut.Backend.InOut
{
    public class MyTypeInOutMock : MyTypeInOut<IType>
    {

        public MyTypeInOutMock(IPlaceHolderNodeType parentNode, InOutSide side, InOutType inOutType, IType concreteType) : base(parentNode, side, inOutType, concreteType)
        {
        }
    } 
}
