using Backend.Connection;
using Backend.IO;
using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class ReturnNode : BuildInNode
    {
        private AutoIO _returnIn;

        public ReturnNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _returnIn = new AutoIO(this, IOSide.Input, true);
            AddInputs(_prevNode, _returnIn);
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
