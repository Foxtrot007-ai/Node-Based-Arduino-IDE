using System;
using Backend.Connection;
using Backend.Template;
using Backend.Type;

namespace Backend.Node
{
    public class FunctionNode : BaseNode
    {
        private FunctionTemplate _functionTemplate;
        public override string NodeName => _functionTemplate.Name;
        public override NodeType NodeType => NodeType.Function;

        public FunctionNode(FunctionTemplate functionTemplate)
        {
            _functionTemplate = functionTemplate;
            if (_functionTemplate.OutputType.EType == EType.Void)
            {
                AddFlowInputs();
            }
            else
            {
                AddOutputs(new AnyInOut(this, InOutSide.Output, _functionTemplate.OutputType));
            }

            _functionTemplate.InputsTypes
                .ForEach(type => AddInputs(new AnyInOut(this, InOutSide.Input, type)));
        }

        protected bool _isFlow()
        {
            return OutputsList[0].InOutType == InOutType.Flow;
        }

        protected virtual string BuildCode(CodeManager codeManager)
        {
            var codeParam = codeManager.BuildParamCode(GetWithoutFlow(InputsList));
            return $"{NodeName}({codeParam})";
        }

        protected override void MakeCode(CodeManager codeManager)
        {
            if (!IsFlow())
                throw new NotImplementedException();
            
            codeManager.AddLibrary(_functionTemplate.Library);
            codeManager.AddLine(BuildCode(codeManager) + ";");
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            if (IsFlow())
                throw new NotImplementedException();

            codeManager.AddLibrary(_functionTemplate.Library);
            return BuildCode(codeManager);
        }
    }
}
