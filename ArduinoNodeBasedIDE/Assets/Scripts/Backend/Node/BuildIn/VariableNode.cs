using Backend.Connection;
using Backend.IO;
using Backend.Type;
using Backend.Variables;

namespace Backend.Node.BuildIn
{
    public abstract class VariableNode : BaseNode
    {
        protected Variable _variable;
        protected TypeIO _value;

        protected VariableNode(Variable variable, IOSide side, string id) : base(id)
        {
            _variable = variable;
            _value = new TypeIO(this, side, variable.Type);
            variable.AddRef(this);
        }

        public virtual void ChangeType(IType type)
        {
            _value.ChangeType(type);
        }

        public override void Delete()
        {
            base.Delete();
            _variable.DeleteRef(this);
        }
    }
}
