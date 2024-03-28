using System.Collections.Generic;
using Backend.InOut;
using Backend.InOut.Base;
using Backend.Type;

namespace Backend.Node
{
    public class IfNode : BaseNode
    {

        private readonly FlowInOut _previousNode;
        private readonly FlowInOut _nextNode;
        private readonly FlowInOut _true;
        private readonly FlowInOut _false;
        private readonly PrimitiveInOut _predicate;

        public IfNode()
        {
            NodeName = "IF";
            NodeType = NodeType.If;
            _previousNode = new FlowInOut(this, InOutSide.Input, "prev");
            _predicate = new PrimitiveInOut(this, InOutSide.Input, new PrimitiveType(EType.Bool));
            InputsListInOut = new List<IInOut>
            {
                _previousNode,
                _predicate,
            };

            _nextNode = new FlowInOut(this, InOutSide.Output, " next");
            _true = new FlowInOut(this, InOutSide.Output, " true");
            _false = new FlowInOut(this, InOutSide.Output, " false");

            OutputsListInOut = new List<IInOut>
            {
                _nextNode,
                _true,
                _false
            };
        }

        public IfNode(FlowInOut previousNode)
        {
            _previousNode = previousNode;
        }

        public string ToCode()
        {
            return "";
        }
    }
}