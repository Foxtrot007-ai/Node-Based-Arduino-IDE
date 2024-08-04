using Backend.Connection;
using Backend.IO;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class CompareNode : BuildInNode, ISubscribeIO
    {
        private CompareOpTemplate _compareTemplate;
        private AutoIO _in1;
        private AutoIO _in2;

        private int _connectionCounter = 0;

        public CompareNode(CompareOpTemplate compareTemplate) : base(compareTemplate)
        {
            _compareTemplate = compareTemplate;

            _in1 = new AutoIO(this, IOSide.Input);
            _in1.Subscribe(this);
            _in2 = new AutoIO(this, IOSide.Input);
            _in2.Subscribe(this);
            AddInputs(_in1, _in2);
            AddOutputs(new TypeIO(this, IOSide.Output, new PrimitiveType(EType.Bool)));
        }
        public void ConnectNotify(BaseIO baseIO)
        {
            _connectionCounter++;
            if (_connectionCounter != 1) return;

            _in1.ChangeType(((TypeIO)baseIO).MyType);
            _in2.ChangeType(((TypeIO)baseIO).MyType);
        }
        public void DisconnectNotify(BaseIO baseIO)
        {
            _connectionCounter--;
            if (_connectionCounter != 0) return;
            _in1.ResetMyType();
            _in2.ResetMyType();
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            return $"{ConnectedToCodeParam(codeManager, _in1)} {_compareTemplate.ToCode()} {ConnectedToCodeParam(codeManager, _in2)}";
        }
    }
}
