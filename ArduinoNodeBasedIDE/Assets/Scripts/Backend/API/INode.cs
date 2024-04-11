using System;
using System.Collections.Generic;
using Backend.API;
using Backend.Node;

namespace Backend
{
    public interface INode
    {
        public List<IConnection> InputsList { get; }
        public List<IConnection> OutputsList { get; }
        public NodeType NodeType { get; }
        public string NodeName { get; }
        public bool IsDeleted { get; }  // If true node is logical deleted and need to be physical delete
        public void Delete();
    }
}
