using Backend.Connection;
using Backend.Node;
using Backend.Type;

namespace Tests.EditMode.ut.Backend.Connection
{
    public class MyTypeInOutMock : MyTypeInOut
    {
        public MyTypeInOutMock(IPlaceHolderNodeType parentNode, InOutSide side, IType myType) : base(parentNode, side, myType)
        {
        }
    } 
}
