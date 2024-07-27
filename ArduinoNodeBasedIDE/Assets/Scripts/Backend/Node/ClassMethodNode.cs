using System.Linq;
using Backend.Connection;
using Backend.Template;

namespace Backend.Node
{
    public class ClassMethodNode : FunctionNode
    {
        private readonly ClassMethodTemplate _classMethodTemplate;
        private TypeInOut _classIn;
        public override NodeType NodeType => NodeType.Method;

        public ClassMethodNode(ClassMethodTemplate classMethodTemplate) : base(classMethodTemplate)
        {
            _classMethodTemplate = classMethodTemplate;
            _classIn = new TypeInOut(this, InOutSide.Input, _classMethodTemplate.Class);
            InputsList.Insert(_isFlow() ? 1 : 0, _classIn);
        }

        protected override string BuildCode(CodeManager codeManager)
        {
            var codeParam = codeManager.BuildParamCode(GetWithoutFlow(InputsList).Skip(1));
            return $"{ConnectedToCodeParam(codeManager, _classIn)}.{NodeName}({codeParam})";
        }
    }
}
