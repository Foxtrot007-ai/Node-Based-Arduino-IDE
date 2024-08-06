using Backend.Connection;
using Backend.IO;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class NotNode : BuildInNode
    {
        private TypeIO _in;

        protected NotNode()
        {
        }

        public NotNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _in = new TypeIO(this, IOSide.Input, new PrimitiveType(EType.Bool));
            AddInputs(_in);
            AddOutputs(new TypeIO(this, IOSide.Output, new PrimitiveType(EType.Bool)));
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            return $"!{ConnectedToCodeParam(codeManager, _in)}";
        }
    }
}
