using System;
using Backend.API;
using Backend.IO;
using Backend.MyFunction;
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
        public UserFunctionNode(UserFunction userFunction) : base(userFunction.PathName)
        {
            _userFunction = userFunction;
            _userFunction.AddRef(this);

            _userFunction.InputList
                .Variables
                .ForEach(param => AddInputs(new TypeIO(this, IOSide.Input, (IType)param.Type)));

            if (userFunction.OutputType.EType == EType.Void)
            {
                AddFlowInputs();
            }
            else
            {
                AddOutputs(new TypeIO(this, IOSide.Output, (IType)userFunction.OutputType));
            }
        }

        private TypeIO GetParamByIndex(int index)
        {
            return (TypeIO)InputsList[IsFlow() ? index + 1 : index];
        }
        public virtual void AddParam(IVariable param)
        {
            AddInputs(new TypeIO(this, IOSide.Input, (IType)param.Type));
        }

        public virtual void RemoveParam(int index)
        {
            RemoveInOut(GetParamByIndex(index));
        }

        public virtual void ChangeParam(int index, IVariable variable)
        {
            GetParamByIndex(index).ChangeType((IType)variable.Type);
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
                    AddOutputs(new TypeIO(this, IOSide.Output, (IType)type));
                }
                else
                {
                    ((TypeIO)OutputsList[0]).ChangeType((IType)type);
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
