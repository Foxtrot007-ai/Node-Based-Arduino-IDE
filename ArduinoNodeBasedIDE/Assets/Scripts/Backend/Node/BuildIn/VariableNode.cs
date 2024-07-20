using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public abstract class VariableNode : BaseNode
    {
        protected Variable _variable;
        protected AnyInOut _value;

        protected VariableNode(Variable variable, InOutSide side)
        {
            _variable = variable;
            _value = new AnyInOut(this, side, variable.Type);
            variable.AddRef(this);
        }

        protected override void CheckToCode()
        {
            CheckIfConnected(_value);
        }

        public virtual void ChangeType(IType type)
        {
            _value.ChangeMyType(type);
        }

        public override void Delete()
        {
            base.Delete();
            _variable.DeleteRef(this);
        }
    }
}
