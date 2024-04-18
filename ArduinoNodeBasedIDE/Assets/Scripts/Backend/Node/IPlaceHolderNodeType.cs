using System.Collections.Generic;
using Backend.Connection;

namespace Backend.Node
{
    public interface IPlaceHolderNodeType : INode
    {
        public List<InOut> InputsListInOut { get; }
        public List<InOut> OutputsListInOut { get; }
    }
}
