﻿using Backend.Connection;
using Backend.Connection.MyType;
using Backend.Type;

namespace Backend.Node
{
    public class IfNode : BaseNode
    {

        private readonly FlowInOut _true;
        private readonly FlowInOut _false;
        private readonly PrimitiveInOut _predicate;

        public override string NodeName => "If";
        public override NodeType NodeType => NodeType.If;

        public IfNode()
        {
            AddFlowInputs();
            _predicate = new PrimitiveInOut(this, InOutSide.Input, new PrimitiveType(EType.Bool));
            AddInputs(_predicate);
            
            _true = new FlowInOut(this, InOutSide.Output, " true");
            _false = new FlowInOut(this, InOutSide.Output, " false");

            AddOutputs(_true, _false);
        }

        protected override void CheckToCode()
        {
            CheckFlowConnected();
            CheckIfConnected(_predicate);
            CheckIfConnected(_true);
        }

        private string FalseToCode()
        {
            string str = "{"
                         + ConnectedToCode(_false)
                         + "}";
            return _false.Connected is null ? "" : str;
        }
        public override string ToCode()
        {
            CheckToCode();
            return $"if ({ConnectedToCode(_predicate)})"
                   + "{"
                   + ConnectedToCode(_true)
                   + "}"
                   + FalseToCode()
                   + ConnectedToCode(_nextNode);
        }
    }
}
