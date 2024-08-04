using Backend.Connection;

namespace Backend.API
{
    public interface IConnection
    {
        /*
         * Return other side of Connected
         * Null if other side non exists
         */
        public IConnection Connected { get; }

        /*
         * Type of InOut
         * Multiple implementation of IConnection might have same InOutType
         */
        public IOType IOType { get; }

        /*
         * InOutType as string
         * For FlowInOut name of connection
         */
        public string IOName { get; }
        
        /*
         * Return parent
         */
        public INode ParentNode { get; }
        
        /*
         * Make validation and might throw specific exception:
         * AlreadyConnected
         * SelfConnected
         * SameSide
         * Cycle when cycle is detected
         * WrongConnectionType (e.g. FlowIO connected to TypeIO
         * CannotBeCast
         * AdapterNeed ? - might be remove
         * 
         * After success validation make connection between IConnection
         * Don't call on other side (will throw AlreadyConnectedException)
         */
        public void Connect(IConnection iConnection);

        /*
         * Remove connection between IConnection
         * No need to call on other side
         */
        public void Disconnect();

        /*
         * If true IConnection is logicalDelete, need physical delete
         */
        public bool IsDeleted { get; }

        /*
         * Null if connection is new
         * Need to be set by UI
         */
        public ConnectionPoint UIPoint { get; set; }
    }
}
