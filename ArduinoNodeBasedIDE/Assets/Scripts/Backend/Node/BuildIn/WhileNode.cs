using Backend.Connection;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class WhileNode : BuildInNode
    {
        private TypeInOut _predicate;
        private FlowInOut _loop;
        
        public WhileNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _predicate = new TypeInOut(this, InOutSide.Input, new PrimitiveType(EType.Bool));
            AddInputs(_prevNode, _predicate);
 
            _loop = new FlowInOut(this, InOutSide.Output, "body");
            AddOutputs(_nextNode, _loop);
        }

        protected override void MakeCode(CodeManager codeManager)
        {
            codeManager.AddLine($"while ({ConnectedToCodeParam(codeManager, _predicate)})");

            var loopCopy = new CodeManager(codeManager);
            ConnectedToCode(loopCopy, _loop);
            codeManager.AddLines(loopCopy.CodeLines);
        }
    }
}
