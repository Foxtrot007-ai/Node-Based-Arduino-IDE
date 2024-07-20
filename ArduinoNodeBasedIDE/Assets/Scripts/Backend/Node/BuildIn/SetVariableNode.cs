using Backend.Connection;

namespace Backend.Node.BuildIn
{
    public class SetVariableNode : VariableNode
    {
        public override string NodeName => "Set" + _variable.Name;

        public SetVariableNode(Variable variable) : base(variable, InOutSide.Input)
        {
            AddInputs(_value);
        }

        protected override void CheckToCode()
        {
            base.CheckToCode();
            CheckFlowConnected();
        }

        public override void ToCode(CodeManager codeManager)
        {
            CheckToCode();
            string prefix = "";
            if (codeManager.GetVariableStatus(_variable) == CodeManager.VariableStatus.Unknown)
            {
                codeManager.SetVariableStatus(_variable, CodeManager.VariableStatus.Set);
                prefix = _variable.Type.ToCode() + " ";
            }

            codeManager.AddLine($"{prefix}{_variable.Name} = {ConnectedToCodeParam(codeManager, _value)};");
            NextToCode(codeManager);
        }
    }
}
