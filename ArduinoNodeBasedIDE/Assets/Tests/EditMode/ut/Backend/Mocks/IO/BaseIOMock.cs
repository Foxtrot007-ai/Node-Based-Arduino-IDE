using Backend.IO;
using Backend.Node;
using NSubstitute;

namespace Tests.EditMode.ut.Backend.Mocks.IO
{
    public class BaseIOMock : BaseIO
    {
        public override IOType IOType { get; }
        public override string IOName { get; }
        public BaseIOMock(BaseNodeMock parentNode, IOSide side) : base(parentNode, side)
        {
        }

        public BaseIOMock() : base(Substitute.For<BaseNode>(), IOSide.Input)
        {
            
        }
    }
}