using Backend.Connection;
using Backend.Type;
using Tests.EditMode.ut.Backend.Mocks;
using Tests.EditMode.ut.Backend.Node;

namespace Tests.EditMode.ut.Backend.Connection
{
    public class MyTypeInOutMock : MyTypeInOut
    {
        public MyTypeInOutMock(BaseNodeMock parentNode, InOutSide side, IType myType) : base(parentNode, side, myType)
        {
        }
    } 
}
