using System;
using System.Collections.Generic;
using Backend.Node;

namespace Backend
{
    public interface INode
    {
        public void Delete();
        public List<IInOut> InputsList { get; }
        public List<IInOut> OutputsList { get; }
        public NodeType NodeType { get; }
        public String NodeName { get; }
    }
}
