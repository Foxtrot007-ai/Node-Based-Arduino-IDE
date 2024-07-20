using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Type;

namespace Backend.Node
{
    public class IfNode : BaseNode
    {

        private readonly FlowInOut _true;
        private readonly FlowInOut _false;
        private readonly PrimitiveInOut _predicate;

        public override string NodeName => "If";
        public override NodeType NodeType => NodeType.If;

        public IfNode()
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

        public override void ToCode(CodeManager codeManager)
        {
            CheckToCode();
            codeManager.AddLine($"if ({ConnectedToCodeParam(codeManager, _predicate)})");

            var trueCopy = new CodeManager(codeManager);
            ConnectedToCode(trueCopy, _true);
            codeManager.AddLines(trueCopy.CodeLines);

            if (_false is not null)
            {
                var falseCopy = new CodeManager(codeManager);
                ConnectedToCode(falseCopy, _false);
                codeManager.AddLines(falseCopy.CodeLines);
            }

            NextToCode(codeManager);
        }
    }
}
