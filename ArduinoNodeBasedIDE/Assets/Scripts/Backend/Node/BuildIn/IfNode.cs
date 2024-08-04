using Backend.Connection;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class IfNode : BuildInNode
    {
        private FlowIO _true;
        private FlowIO _false;
        private TypeIO _predicate;

        public IfNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            AddFlowInputs();
            _predicate = new TypeIO(this, IOSide.Input, new PrimitiveType(EType.Bool));
            AddInputs(_predicate);

            _true = new FlowIO(this, IOSide.Output, " true");
            _false = new FlowIO(this, IOSide.Output, " false", true);

            AddOutputs(_true, _false);
        }

        protected override void MakeCode(CodeManager codeManager)
        {
            codeManager.AddLine($"if ({ConnectedToCodeParam(codeManager, _predicate)})");

            var trueCopy = new CodeManager(codeManager);
            ConnectedToCode(trueCopy, _true);
            codeManager.AddLines(trueCopy.CodeLines);

            if (_false.Connected is not null)
            {
                codeManager.AddLine("else");
                var falseCopy = new CodeManager(codeManager);
                ConnectedToCode(falseCopy, _false);
                codeManager.AddLines(falseCopy.CodeLines);
            }
        }
    }
}
