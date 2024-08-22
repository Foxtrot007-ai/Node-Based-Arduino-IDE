using Backend.API;
using Backend.IO;
using Backend.MyFunction;
using Backend.Template;
using Backend.Type;

namespace Backend.Node.BuildIn
{
    public class ReturnNode : BuildInNode
    {
        private TypeIO _returnIn;
        private Function _function;

        protected ReturnNode() : base()
        {
        }
        public ReturnNode(BuildInTemplate buildInTemplate, Function function) : base(buildInTemplate)
        {
            _function = function;
            AddInputs(_prevNode);
            if (_function.OutputType.EType != EType.Void)
            {
                _returnIn = new TypeIO(this, IOSide.Input, (IType)function.OutputType);
                AddInputs(_returnIn);
            }

            _function.AddReturnRef(this);
        }

        public override void ToCode(CodeManager codeManager)
        {
            var suffix = "";
            if (_function.OutputType.EType != EType.Void)
            {
                suffix = ConnectedToCodeParam(codeManager, _returnIn);
            }

            codeManager.AddLine($"return {suffix};");
        }

        public virtual void ChangeInputType(IMyType type)
        {
            if (type.EType == EType.Void)
            {
                RemoveInOut(_returnIn);
                _returnIn = null;
            }
            else
            {
                if (_returnIn is null)
                {
                    _returnIn = new TypeIO(this, IOSide.Input, (IType)type);
                    AddInputs(_returnIn);
                }
                else
                {
                    _returnIn.ChangeType((IType)type);
                }
            }
        }

        public override void Delete()
        {
            base.Delete();
            _function.RemoveReturnRef(this);
        }
    }
}
