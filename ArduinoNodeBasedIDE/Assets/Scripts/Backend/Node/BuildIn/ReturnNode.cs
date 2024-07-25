using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class ReturnNode : BuildInNode
    {
        private AutoInOut _returnIn;

        public ReturnNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _returnIn = new AutoInOut(this, InOutSide.Input);
            AddInputs(_prevNode, _returnIn);
        }

        protected override void CheckToCode()
        {
            CheckIfConnected(_prevNode);
        }

        public override void ToCode(CodeManager codeManager)
        {
            var suffix = "";
            if (_returnIn.Connected is not null)
            {
                suffix = ConnectedToCodeParam(codeManager, _returnIn);
            }

            codeManager.AddLine($"return {suffix};");
        }
    }
}
