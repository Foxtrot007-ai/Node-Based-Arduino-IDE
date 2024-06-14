using Backend.Connection;

namespace Tests.EditMode.ut.Backend.Mocks
{
    public class InOutMock : InOut
    {
        public override InOutType InOutType { get; }
        public override string InOutName { get; }
        public InOutMock(BaseNodeMock parentNode, InOutSide side) : base(parentNode, side)
        {
        }

        public InOutMock() : base(null, InOutSide.Input)
        {
            
        }
    }
}