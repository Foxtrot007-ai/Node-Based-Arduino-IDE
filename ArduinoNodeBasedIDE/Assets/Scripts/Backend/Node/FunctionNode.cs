using System;
using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Template;
using Backend.Type;

namespace Backend.Node
{
    public class FunctionNode : BaseNode
    {
        private FunctionTemplate _functionTemplate;
        public override string NodeName => _functionTemplate.Name;
        public override NodeType NodeType => NodeType.Function;

        protected FunctionNode()
        {
        }

        public FunctionNode(FunctionTemplate functionTemplate)
        {
            _functionTemplate = functionTemplate;
            if (_functionTemplate.OutputType.EType == EType.Void)
            {
                AddFlowInputs();
            }
            else
            {
                OutputsList.Add(new AnyInOut(this, InOutSide.Output, _functionTemplate.OutputType));
            }

            _functionTemplate.InputsTypes.ForEach(x => InputsList.Add(new AnyInOut(this, InOutSide.Input, x)));
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

        public override void ToCode(CodeManager codeManager)
        {
            if (!_isFlow())
                throw new NotImplementedException();

            CheckToCode();
            codeManager.AddLibrary(_functionTemplate.Library);
            codeManager.AddLine(BuildCode(codeManager) + ";");
            NextToCode(codeManager);
        }

        public override string ToCodeParam(CodeManager codeManager)
        {
            if (_isFlow())
                throw new NotImplementedException();

            codeManager.AddLibrary(_functionTemplate.Library);
            CheckToCode();
            return BuildCode(codeManager);
        }
    }
}
