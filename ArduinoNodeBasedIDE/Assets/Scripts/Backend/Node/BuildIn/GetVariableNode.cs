using Backend.Connection;
using Backend.Exceptions;

namespace Backend.Node.BuildIn
{
    public class GetVariableNode : VariableNode
    {
        public override string NodeName => "Get" + _variable.Name;

        public GetVariableNode(Variable variable) : base(variable, InOutSide.Output)
        {
            AddOutputs(_value);
        }

        public override string ToCodeParam(CodeManager codeManager)
        {
            CheckToCode();
            if (codeManager.GetVariableStatus(_variable) == CodeManager.VariableStatus.Unknown)
            {
                throw new VariableNotSetException();
            }

            return _variable.Name;
        }
    }
}
