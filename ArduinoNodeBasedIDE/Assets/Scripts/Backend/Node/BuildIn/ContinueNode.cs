using Backend.Template;

namespace Backend.Node.BuildIn
{
    public class ContinueNode : BuildInNode
    {
        protected ContinueNode()
        {
        }
        
        public ContinueNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            AddInputs(_prevNode);
        }

        public override void ToCode(CodeManager codeManager)
        {
            codeManager.AddLine("continue;");
        }
    }
}
