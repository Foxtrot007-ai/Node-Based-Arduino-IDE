using System.Collections.Generic;
using System.Linq;
using Backend.API;
using Backend.Connection;

namespace Backend.Node
{
    public abstract class BaseNode : IPlaceHolderNodeType
    {

        public string NodeName { get; protected set;}
        public bool IsDeleted { get; private set; }
        public NodeType NodeType { get; protected set;}
        public List<IConnection> InputsList => (List<IConnection>)InputsListInOut.Cast<IConnection>();
        public List<IConnection> OutputsList => (List<IConnection>)OutputsListInOut.Cast<IConnection>();
        public List<InOut> InputsListInOut { get; protected set; }
        public List<InOut> OutputsListInOut { get; protected set;}
        
        public void Delete()
        {
            InputsListInOut.ForEach(x => x.Delete());
            OutputsListInOut.ForEach(x => x.Delete());
            IsDeleted = true;
        }
    }
}
