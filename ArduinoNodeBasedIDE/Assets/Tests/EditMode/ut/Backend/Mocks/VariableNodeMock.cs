using Backend;
using Backend.IO;
using Backend.Node.BuildIn;
using Backend.Variables;

namespace Tests.EditMode.ut.Backend.mocks
{
    public class VariableNodeMock : VariableNode
    {
        public VariableNodeMock(Variable manager) : base(manager, IOSide.Input, new PathName("test"))
        {
        }
    }
}
