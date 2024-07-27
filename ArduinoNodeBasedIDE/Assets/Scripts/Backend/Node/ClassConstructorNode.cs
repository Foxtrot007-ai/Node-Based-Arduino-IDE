using System.Linq;
using Backend.Connection;
using Backend.Template;

namespace Backend.Node
{
    public class ClassConstructorNode : BaseNode
    {

        private readonly ClassConstructorTemplate _classConstructorTemplate;

        public ClassConstructorNode(ClassConstructorTemplate classConstructorTemplate)
        {
            _classConstructorTemplate = classConstructorTemplate;

            _classConstructorTemplate.Inputs
                .ForEach(type => AddInputs(new AnyInOut(this, InOutSide.Input, type)));

            AddOutputs(new TypeInOut(this, InOutSide.Input, _classConstructorTemplate.Class));
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            codeManager.AddLibrary(_classConstructorTemplate.Library);

            var codeParam = codeManager.BuildParamCode(InputsList);
            return $"new {_classConstructorTemplate.Class.ToCode()}({codeParam})";
        }
    }
}
