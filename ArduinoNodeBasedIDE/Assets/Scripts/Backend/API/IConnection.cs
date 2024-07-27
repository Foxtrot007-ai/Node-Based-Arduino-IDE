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
         * Make validation:
         *  Standard check:
         *    ConnectionType (e.g. FlowInOut cannot be connected to MyInOut)
         *    Connected == null
         *    different side
         *    self connection
         *    cycle
         *  MyTypeInOut check:
         *    cast (e.g. ClassType cannot be cast to StringType)
         *    adapter
         *
         * If validation fail throw specific exception about fail (look Exceptions/InOut)
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
