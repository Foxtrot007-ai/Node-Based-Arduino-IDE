using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Template;

namespace Backend.Node
{
    public class ClassConstructorNode : BaseNode
    {

        private ClassConstructorTemplate _classConstructorTemplate;

        public ClassConstructorNode(ClassConstructorTemplate classConstructorTemplate)
        {
            _classConstructorTemplate = classConstructorTemplate;

            foreach (var input in _classConstructorTemplate.Inputs)
            {
                InputsList.Add(new AnyInOut(this, InOutSide.Input, input));
            }

            OutputsList.Add(new ClassInOut(this, InOutSide.Input, _classConstructorTemplate.Class));
        }

        public override string ToCodeParam(CodeManager codeManager)
        {
            CheckToCode();
            codeManager.AddLibrary(_classConstructorTemplate.Library);

            var codeParam = codeManager.BuildParamCode(InputsList);
            return $"new {_classConstructorTemplate.Class.ToCode()}({codeParam})";
        }
    }
}
