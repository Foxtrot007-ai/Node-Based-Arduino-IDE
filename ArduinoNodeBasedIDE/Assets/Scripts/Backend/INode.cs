using System;
using System.Collections.Generic;
using Backend.Node;

namespace Backend
{
    public interface INode
    {
        public void Delete();
        public List<IConnection> InputsList { get; }
        public List<IConnection> OutputsList { get; }
        public NodeType NodeType { get; }
        public String NodeName { get; }
    }
}
