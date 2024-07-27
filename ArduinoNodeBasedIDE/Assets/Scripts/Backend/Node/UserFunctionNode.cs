using System;
using Backend.API;
using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Function;
using Backend.Type;

namespace Backend.Node
{
    public class UserFunctionNode : BaseNode
    {
        private UserFunction _userFunction;
        public override string NodeName => _userFunction.Name;

        protected UserFunctionNode()
        {
        }
        public UserFunctionNode(UserFunction userFunction)
        {
            _userFunction = userFunction;
            _userFunction.AddRef(this);

            _userFunction.InputList
                .Variables
                .ForEach(param => AddInputs(new AnyInOut(this, InOutSide.Input, (IType)param.Type)));

            if (userFunction.OutputType.EType == EType.Void)
            {
                AddFlowInputs();
            }
            else
            {
                AddOutputs(new AnyInOut(this, InOutSide.Input, (IType)userFunction.OutputType));
            }
        }

        public virtual void AddParam(IVariable param)
        {
            AddInputs(new AnyInOut(this, InOutSide.Input, (IType)param.Type));
        }

        public virtual void RemoveParam(int index)
        {
            RemoveInOut(InputsList[IsFlow() ? index + 1 : index]);
        }

        public virtual void ChangeOutputType(IMyType type)
        {
            if (type.EType == EType.Void)
            {
                RemoveInOut(OutputsList[0]);
                AddFlowInputs();
            }
            else
            {
                if (IsFlow())
                {
                    RemoveFlowInputs();
                    AddOutputs(new AnyInOut(this, InOutSide.Output, (IType)type));
                }
                else
                {
                    ((AnyInOut)OutputsList[0]).ChangeMyType((IType)type);
                }
            }
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

            codeManager.AddLine(BuildCode(codeManager) + ";");
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            if (IsFlow())
                throw new NotImplementedException();

            return BuildCode(codeManager);
        }

        public override void Delete()
        {
            base.Delete();
            _userFunction.DeleteRef(this);
        }
    }
}
