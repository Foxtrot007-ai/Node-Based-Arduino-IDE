using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class StartNode : BuildInNode
    {
        public StartNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            AddOutputs(_nextNode);
        }

        protected override void MakeCode(CodeManager codeManager)
        {
        }
    }
}
