using Backend.Connection;
using Backend.Template;

namespace Backend.Node
{
    public class ClassConstructorNode : BaseNode
    {

        private readonly ClassConstructorTemplate _classConstructorTemplate;
        public override string CreatorId => _classConstructorTemplate.Id;

        public ClassConstructorNode(ClassConstructorTemplate classConstructorTemplate)
        {
            _classConstructorTemplate = classConstructorTemplate;

            _classConstructorTemplate.Inputs
                .ForEach(type => AddInputs(new TypeIO(this, IOSide.Input, type)));

            AddOutputs(new TypeIO(this, IOSide.Input, _classConstructorTemplate.Class));
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            codeManager.AddLibrary(_classConstructorTemplate.Library);

            var codeParam = codeManager.BuildParamCode(InputsList);
            return $"{_classConstructorTemplate.Class.ToCode()}({codeParam})";
        }
    }
}
