using System.Linq;
using Backend.IO;
using Backend.Template;

namespace Backend.Node
{
    public class ClassMethodNode : FunctionNode
    {
        private readonly ClassMethodTemplate _classMethodTemplate;
        private TypeIO _classIn;
        public override string CreatorId => _classMethodTemplate.Id;

        public ClassMethodNode(ClassMethodTemplate classMethodTemplate) : base(classMethodTemplate)
        {
            _classMethodTemplate = classMethodTemplate;
            _classIn = new TypeIO(this, IOSide.Input, _classMethodTemplate.Class);
            InputsList.Insert(_isFlow() ? 1 : 0, _classIn);
        }

        protected override string BuildCode(CodeManager codeManager)
        {
            var codeParam = codeManager.BuildParamCode(GetWithoutFlow(InputsList).Skip(1));
            return $"{ConnectedToCodeParam(codeManager, _classIn)}.{NodeName}({codeParam})";
        }
    }
}
