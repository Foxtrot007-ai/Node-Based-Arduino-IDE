using Backend.IO;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class WhileNode : BuildInNode
    {
        private TypeIO _predicate;
        private FlowIO _loop;

        public WhileNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _predicate = new TypeIO(this, IOSide.Input, new PrimitiveType(EType.Bool));
            AddInputs(_prevNode, _predicate);

            _loop = new FlowIO(this, IOSide.Output, "body");
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
