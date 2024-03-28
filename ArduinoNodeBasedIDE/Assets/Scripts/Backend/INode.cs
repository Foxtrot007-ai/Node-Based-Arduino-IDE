using System;
using System.Collections.Generic;
using Backend.Node;

namespace Backend
{
    public interface INode //UI
    {
        public string NodeName { get; }
        public NodeType NodeType { get; }
        public List<IConnection> InputsList { get; }
        public List<IConnection> OutputsList { get; }
        public void Delete();
    }
}
