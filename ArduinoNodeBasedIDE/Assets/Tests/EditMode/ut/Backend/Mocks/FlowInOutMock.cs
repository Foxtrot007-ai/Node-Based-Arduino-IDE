using Backend.Connection;

namespace Tests.EditMode.ut.Backend.Mocks
{
    public class FlowInOutMock : FlowInOut
    {

        public FlowInOutMock() : base(null, InOutSide.Input, "test")
        {
            
        }
    }
}
