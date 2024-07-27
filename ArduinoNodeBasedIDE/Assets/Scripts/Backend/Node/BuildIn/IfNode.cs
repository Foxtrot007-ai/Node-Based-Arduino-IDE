using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Node.BuildIn;
using Backend.Template;
using Backend.Type;

namespace Backend.Node
{
    public class IfNode : BuildInNode
    {
        private FlowInOut _true;
        private FlowInOut _false;
        private PrimitiveInOut _predicate;
        public override NodeType NodeType => NodeType.If;

        public IfNode (BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            AddFlowInputs();
            _predicate = new PrimitiveInOut(this, InOutSide.Input, new PrimitiveType(EType.Bool));
            AddInputs(_predicate);

            _true = new FlowInOut(this, InOutSide.Output, " true");
            _false = new FlowInOut(this, InOutSide.Output, " false");

            AddOutputs(_true, _false);
        }

        protected override void CheckToCode()
        {
            CheckFlowConnected();
            CheckIfConnected(_predicate);
            CheckIfConnected(_true);
        }

        protected override void MakeCode(CodeManager codeManager)
        {
            codeManager.AddLine($"if ({ConnectedToCodeParam(codeManager, _predicate)})");

            var trueCopy = new CodeManager(codeManager);
            ConnectedToCode(trueCopy, _true);
            codeManager.AddLines(trueCopy.CodeLines);

            if (_false.Connected is not null)
            {
                var falseCopy = new CodeManager(codeManager);
                ConnectedToCode(falseCopy, _false);
                codeManager.AddLines(falseCopy.CodeLines);
            }
        }
    }
}
