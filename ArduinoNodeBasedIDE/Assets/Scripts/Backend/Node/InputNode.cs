using System;
using Backend.API;
using Backend.API.DTO;
using Backend.Exceptions;
using Backend.IO;
using Backend.Node.BuildIn;
using Backend.Template;
using Backend.Type;

namespace Backend.Node
{
    public class InputNode : BuildInNode, IInputNode
    {
        public string Value { get; private set; }
        public IMyType Type => _output.MyType;

        private TypeIO _output;
        public override NodeType NodeType => NodeType.Input;

        protected InputNode()
        {
        }
        public InputNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _output = new TypeIO(this, IOSide.Output, new PrimitiveType(EType.Int));
            AddOutputs(_output);
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            if (!string.IsNullOrEmpty(Value)) return _output.MyType.EType == EType.String ? $"\"{Value}\"" : Value;

            switch (_output.MyType.EType)
            {
                case EType.String:
                    return "\"\"";
                case EType.Short:
                case EType.Int:
                case EType.Long:
                case EType.LongLong:
                case EType.Float:
                case EType.Double:
                    return "0";
                case EType.Bool:
                    return "false";
                case EType.Void:
                case EType.Class:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetValue(InputNodeValueDto inputNodeValueDto)
        {
            if (!inputNodeValueDto.IsDtoValid())
            {
                throw new InvalidInputNodeValueDto();
            }

            Value = inputNodeValueDto.Value;
            _output.ChangeType((IType)inputNodeValueDto.Type);
        }
    }
}
