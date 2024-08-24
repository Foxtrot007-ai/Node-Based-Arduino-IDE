using Backend.IO;
using Backend.Template;

namespace Backend.Node
{
    public class ConstNode : BaseNode
    {
        private ConstTemplate _constTemplate;
        public override string NodeName => _constTemplate.Name;
        public override string CreatorId => _constTemplate.Id;

        public ConstNode(ConstTemplate constTemplate)
        {
            _constTemplate = constTemplate;
            AddOutputs(new TypeIO(this, IOSide.Output, constTemplate.Type));
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            codeManager.AddLibrary(_constTemplate.Library);
            return _constTemplate.Name;
        }
    }
}
