using System;
using Backend.API;
using Backend.Connection;
using Backend.Exceptions.InOut;
using Backend.IO;
using Backend.Node.BuildIn;
using Backend.Template;
using Backend.Type;
using Castle.Core.Internal;

namespace Backend.Node
{
    public class InputNode : BuildInNode, IInputNode, ISubscribeIO
    {
        public string Value { get; private set; }
        private AutoIO _output;
        public override NodeType NodeType => NodeType.Input;

        protected InputNode()
        {
        }
        public InputNode(BuildInTemplate buildInTemplate) : base(buildInTemplate)
        {
            _output = new AutoIO(this, IOSide.Output, false);
            _output.Subscribe(this);
            AddOutputs(_output);
        }

        public void SetValue(string value)
        {
            if (!IsValueValid(value))
            {
                throw new WrongConnectionTypeException();
            }
            Value = value;
        }
        
        public void ConnectNotify(TypeIO typeIO)
        {
            if (!IsValueValid(Value))
            {
                _output.Disconnect();
                throw new WrongConnectionTypeException();
            }
        }

        private bool IsValueValid(string value)
        {
            if (_output.Connected is null || value.IsNullOrEmpty())
            {
                return true;
            }

            switch (_output.MyType.EType)
            {
                case EType.String:
                    return true;
                case EType.Short:
                case EType.Int:
                case EType.Long:
                case EType.LongLong:
                    return long.TryParse(value, out _);
                case EType.Float:
                    return float.TryParse(value, out _);
                case EType.Double:
                    return double.TryParse(value, out _);
                case EType.Bool:
                    return bool.TryParse(value, out _);
                case EType.Void:
                case EType.Class:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void DisconnectNotify(TypeIO typeIO)
        {
        }

        protected override string MakeCodeParam(CodeManager codeManager)
        {
            if (!Value.IsNullOrEmpty()) return _output.MyType.EType == EType.String ? $"\"{Value}\"" : Value;
        
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
        public void TypeChangeNotify(TypeIO typeIO)
        {
        }
    }
}
