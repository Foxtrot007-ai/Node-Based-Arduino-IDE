using Backend.Exceptions;
using Backend.IO;
using Backend.Variables;

namespace Backend.Node.BuildIn
{
    public class GetVariableNode : VariableNode
    {
        public override string NodeName => "Get " + _variable.Name;

        public GetVariableNode(Variable variable) : base(variable, IOSide.Output, new PathName(variable.PathName, "GET"))
        {
            AddOutputs(_value);
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            if (codeManager.GetVariableStatus(_variable) == CodeManager.VariableStatus.Unknown)
            {
                throw new VariableNotSetException();
            }

            return _variable.Name;
        }
    }
}
