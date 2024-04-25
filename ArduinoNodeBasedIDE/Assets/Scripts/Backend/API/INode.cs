using System.Collections.Generic;
using Backend.Node;

namespace Backend.API
{
    public interface INode
    {
        /*
         * Return all inputs in list !!!READ ONLY!!!
         * If input is new will have UIPoint == null
         * If input is deleted will not be on list
         */
        public List<IConnection> InputsList { get; }

        /*
         * Return all outputs in list !!!READ ONLY!!!
         * If input is new will have UIPoint == null
         * If input is deleted will not be on list
         */
        public List<IConnection> OutputsList { get; }

        /*
         * Return type of node
         */
        public NodeType NodeType { get; }

        /*
         * Return name of node
         */
        public string NodeName { get; }

        /*
         * If true node is logicalDelete, need physical delete
         */
        public bool IsDeleted { get; }
        
        /*
         * Node delete
         * Disconnect all connection
         * After success set IsDeleted to true
         */
        public void Delete();
    }
}
