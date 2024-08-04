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

        protected override void MakeCode(CodeManager codeManager)
        {
            codeManager.AddLine("continue;");
        }
    }
}
