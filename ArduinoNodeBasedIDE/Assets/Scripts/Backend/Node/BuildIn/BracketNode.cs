using Backend.Connection;
using Backend.IO;
using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class BracketNode : BuildInNode
    {

        private ChainIO _chainIO1;
        private ChainIO _chainIO2;

        protected BracketNode()
        {
        }
        public BracketNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _chainIO1 = new ChainIO(this, IOSide.Output, false, false);
            AddOutputs(_chainIO1);

            _chainIO2 = new ChainIO(this, IOSide.Input, false, false);
            _chainIO1.AppendChain(_chainIO2);
            AddInputs(_chainIO2);
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            return $"({ConnectedToCodeParam(codeManager, _chainIO2)})";
        }
    }
}
