using System.Collections.Generic;
using Backend.InOut;

namespace Backend.Node
{
    public interface IPlaceHolderNodeType : INode
    {
        public List<IInOut> InputsListInOut { get; }
        public List<IInOut> OutputsListInOut { get; }
    }
}
