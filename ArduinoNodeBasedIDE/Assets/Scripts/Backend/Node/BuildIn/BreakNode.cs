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

        protected override void MakeCode(CodeManager codeManager)
        {
            codeManager.AddLine("break;");
        }
    }
}
