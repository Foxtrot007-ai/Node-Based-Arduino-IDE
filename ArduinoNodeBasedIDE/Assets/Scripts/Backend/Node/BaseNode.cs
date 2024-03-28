using System.Collections.Generic;
using System.Linq;
using Backend.InOut;

namespace Backend.Node
{
    public abstract class BaseNode : IPlaceHolderNodeType
    {

        public string NodeName { get; protected set;}
        public NodeType NodeType { get; protected set;}
        public List<IConnection> InputsList => (List<IConnection>)InputsListInOut.Cast<IConnection>();
        public List<IConnection> OutputsList => (List<IConnection>)OutputsListInOut.Cast<IConnection>();
        public List<IInOut> InputsListInOut { get; protected set; }
        public List<IInOut> OutputsListInOut { get; protected set;}
        
        public void Delete()
        {
            InputsListInOut.ForEach(x => x.Disconnect());
            OutputsListInOut.ForEach(x => x.Disconnect());
        }
    }
}
