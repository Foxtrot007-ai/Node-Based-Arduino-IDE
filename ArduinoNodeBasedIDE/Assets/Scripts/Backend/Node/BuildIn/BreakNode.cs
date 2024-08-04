using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class BreakNode : BuildInNode
    {
        protected BreakNode()
        {
        }
        
        public BreakNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            AddInputs(_prevNode);
        }

        public override void ToCode(CodeManager codeManager)
        {
            codeManager.AddLine("break;");
        }
    }
}
