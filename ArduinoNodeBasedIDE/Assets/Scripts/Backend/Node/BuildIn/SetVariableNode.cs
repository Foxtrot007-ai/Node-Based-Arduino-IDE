using Backend.Connection;

namespace Backend.Node.BuildIn
{
    public class SetVariableNode : VariableNode
    {
        public override string NodeName => "Set" + _variable.Name;

        public SetVariableNode(Variable variable) : base(variable, IOSide.Input)
        {
            AddInputs(_value);
        }

        protected override void MakeCode(CodeManager codeManager)
        {
            string prefix = "";
            if (codeManager.GetVariableStatus(_variable) == CodeManager.VariableStatus.Unknown)
            {
                codeManager.SetVariableStatus(_variable, CodeManager.VariableStatus.Set);
                prefix = _variable.Type.ToCode() + " ";
            }

            codeManager.AddLine($"{prefix}{_variable.Name} = {ConnectedToCodeParam(codeManager, _value)};");
        }
    }
}
