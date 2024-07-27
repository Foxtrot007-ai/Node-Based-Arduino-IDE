using Backend;
using Backend.Connection;
using Backend.Node.BuildIn;

namespace Tests.EditMode.ut.Backend.mocks
{
    public class VariableNodeMock : VariableNode
    {
        public VariableNodeMock(Variable manager) : base(manager, IOSide.Input)
        {
        }
    }
}
